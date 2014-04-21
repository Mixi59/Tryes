using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog;
using Stump.Core.Threading;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.Server.BaseServer.IPC.Messages;
using Stump.Server.BaseServer.Initialization;
using Stump.Server.BaseServer.Network;
using Stump.Server.WorldServer.Core.IPC;
using Stump.Server.WorldServer.Core.Network;
using Stump.Server.WorldServer.Database.Characters;
using Stump.Server.WorldServer.Game;
using Stump.Server.WorldServer.Game.Accounts;
using Stump.Server.WorldServer.Game.Breeds;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Handlers.Characters;

namespace Stump.Server.WorldServer.Handlers.Approach
{
    public class ApproachHandler : WorldHandlerContainer
    {
        public static SynchronizedCollection<WorldClient> ConnectionQueue = new SynchronizedCollection<WorldClient>();

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private static Task m_queueRefresherTask;

        [Initialization(InitializationPass.First)]
        private static void Initialize()
        {
            m_queueRefresherTask = Task.Factory.StartNewDelayed(3000, RefreshQueue);
        }

        private static void RefreshQueue()
        {
            try
            {
                var toRemove = new List<WorldClient>();
                var count = 0;
                lock (ConnectionQueue.SyncRoot)
                {
                    foreach (var worldClient in ConnectionQueue)
                    {
                        count++;

                        if (!worldClient.Connected)
                        {
                            toRemove.Add(worldClient);
                        }

                        if (DateTime.Now - worldClient.InQueueUntil <= TimeSpan.FromSeconds(3))
                            continue;

                        SendQueueStatusMessage(worldClient, (ushort)count, (ushort)ConnectionQueue.Count);
                        worldClient.QueueShowed = true;
                    }

                    foreach (var worldClient in toRemove)
                    {
                        ConnectionQueue.Remove(worldClient);
                    }
                }
            }
            finally 
            {
                m_queueRefresherTask = Task.Factory.StartNewDelayed(3000, RefreshQueue);
            }
        }

        [WorldHandler(AuthenticationTicketMessage.Id, ShouldBeLogged = false, IsGamePacket = false)]
        public static void HandleAuthenticationTicketMessage(WorldClient client, AuthenticationTicketMessage message)
        {
            if (!IPCAccessor.Instance.IsConnected)
            {
                client.Send(new AuthenticationTicketRefusedMessage());
                client.DisconnectLater(1000);
                return;
            }

            logger.Debug("Client request ticket {0}", message.ticket);
            IPCAccessor.Instance.SendRequest<AccountAnswerMessage>(new AccountRequestMessage() { Ticket = message.ticket }, 
                msg => WorldServer.Instance.IOTaskPool.AddMessage(() => OnAccountReceived(msg, client)), error => client.Disconnect());
        }

        private static void OnAccountReceived(AccountAnswerMessage message, WorldClient client)
        {
            lock (ConnectionQueue.SyncRoot)
                ConnectionQueue.Remove(client);

            if (client.QueueShowed)
                SendQueueStatusMessage(client, 0, 0); // close the popup

            var ticketAccount = message.Account;

            /* Check null ticket */
            if (ticketAccount == null)
            {
                client.Send(new AuthenticationTicketRefusedMessage());
                client.DisconnectLater(1000);
                return;
            }

            /* Bind WorldAccount if exist */
            var account = AccountManager.Instance.FindById(ticketAccount.Id);
            if (account != null)
            {
                client.WorldAccount = account;

                if (client.WorldAccount.ConnectedCharacter != null)
                {
                    var character = World.Instance.GetCharacter(client.WorldAccount.ConnectedCharacter.Value);

                    if (character != null)
                        character.LogOut();
                }
            }

            /* Bind Account & Characters */
            client.SetCurrentAccount(ticketAccount);

            /* Ok */
            client.Send(new AuthenticationTicketAcceptedMessage());
            SendServerOptionalFeaturesMessage(client, new short[0]);
            SendAccountCapabilitiesMessage(client);

            client.Send(new TrustStatusMessage(true)); // Restrict actions if account is not trust

            /* Just to get console AutoCompletion */
            if (client.UserGroup.Role >= RoleEnum.Moderator)
                SendConsoleCommandsListMessage(client);

            var characterInFight = FindCharacterFightReconnection(client);
            if (characterInFight != null)
                CharacterHandler.CommonCharacterSelection(client, characterInFight);

        }
        
        private static CharacterRecord FindCharacterFightReconnection(WorldClient client)
        {
            return (from characterInFight in client.Characters.Where(x => x.LeftFightId != null) let fight = FightManager.Instance.GetFight(characterInFight.LeftFightId.Value) where fight != null let fighter = fight.GetLeaver(characterInFight.Id) where fighter != null select characterInFight).FirstOrDefault();
        }

        public static void SendStartupActionsListMessage(IPacketReceiver client)
        {
            client.Send(new StartupActionsListMessage());
        }

        public static void SendServerOptionalFeaturesMessage(IPacketReceiver client, IEnumerable<short> features)
        {
            client.Send(new ServerOptionalFeaturesMessage(features));
        }

        public static void SendAccountCapabilitiesMessage(WorldClient client)
        {
            client.Send(new AccountCapabilitiesMessage(
                            client.Account.Id,
                            false,
                            (short)client.Account.BreedFlags,
                            (short)BreedManager.Instance.AvailableBreedsFlags,
                            (sbyte) client.UserGroup.Role));
        }

        public static void SendConsoleCommandsListMessage(IPacketReceiver client)
        {
            var commands = WorldServer.Instance.CommandManager.AvailableCommands;

            client.Send(
                new ConsoleCommandsListMessage(
                    commands.SelectMany(c => c.Aliases),
                    commands.Select(c => c.GetSafeUsage()), commands.Select(c => c.Description)));
        }
        

        public static void SendQueueStatusMessage(IPacketReceiver client, ushort position, ushort total)
        {
            client.Send(new QueueStatusMessage(position, total));
        }
    }
}