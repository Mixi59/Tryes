
using System;
using System.Collections.Generic;
using System.Linq;
using Stump.DofusProtocol.Classes;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.Server.WorldServer.Entities;
using Stump.Server.WorldServer.Fights;
using Stump.Server.WorldServer.Global;
using Stump.Server.WorldServer.Global.Pathfinding;

namespace Stump.Server.WorldServer.Handlers
{
    public partial class ContextHandler : WorldHandlerContainer
    {
        private ContextHandler()
        {
            Predicates = new Dictionary<Type, Predicate<WorldClient>>
                         {
                             {typeof (GameContextQuitMessage), PredicatesDefinitions.IsFighting},
                             {typeof (GameActionFightCastRequestMessage), PredicatesDefinitions.IsFighting},
                             {typeof (GameFightTurnFinishMessage), PredicatesDefinitions.IsFighting},
                             {typeof (GameFightTurnReadyMessage), PredicatesDefinitions.IsFighting},
                             {typeof (GameFightReadyMessage), PredicatesDefinitions.IsFighting},
                             {typeof (GameFightPlacementPositionRequestMessage), PredicatesDefinitions.IsFighting},
                             {
                                 typeof (GameRolePlayPlayerFightFriendlyAnswerMessage),
                                 entry => PredicatesDefinitions.IsDialogRequested(entry) &&
                                          entry.ActiveCharacter.DialogRequest is FightRequest
                                 },
                             {typeof (NpcDialogReplyMessage), PredicatesDefinitions.IsDialogingWithNpc},
                         };
        }

        [WorldHandler(typeof (GameContextCreateRequestMessage))]
        public static void HandleGameContextCreateRequestMessage(WorldClient client,
                                                                 GameContextCreateRequestMessage message)
        {
            SendGameContextDestroyMessage(client);
            SendGameContextCreateMessage(client, 1);

            CharacterHandler.SendCharacterStatsListMessage(client);
            CharacterHandler.SendLifePointsRegenBeginMessage(client, 60);

            SendCurrentMapMessage(client, client.ActiveCharacter.Map.Id);
            BasicHandler.SendBasicTimeMessage(client);

            World.Instance.SendMessageOfTheDay(client.ActiveCharacter);
        }

        [WorldHandler(typeof (GameMapChangeOrientationRequestMessage))]
        public static void HandleGameMapChangeOrientationRequestMessage(WorldClient client,
                                                                        GameMapChangeOrientationRequestMessage message)
        {
            client.ActiveCharacter.Position.ChangeLocation((DirectionsEnum) message.direction);
            client.ActiveCharacter.Map.Do(
                charac => SendGameMapChangeOrientationMessage(charac.Client, client.ActiveCharacter));
        }

        // todo : get and check whole path
        [WorldHandler(typeof (GameMapMovementRequestMessage))]
        public static void HandleGameMapMovementRequestMessage(WorldClient client, GameMapMovementRequestMessage message)
        {
            if (!client.ActiveCharacter.CanMove())
                return;

            var movementPath = new MovementPath(client.ActiveCharacter.Map, message.keyMovements);

            if (client.ActiveCharacter.IsInFight)
            {
                client.ActiveCharacter.Fighter.Move(movementPath);
            }
            else
            {
                client.ActiveCharacter.Move(movementPath);
            }
        }

        [WorldHandler(typeof (GameMapMovementConfirmMessage))]
        public static void HandleGameMapMovementConfirmMessage(WorldClient client, GameMapMovementConfirmMessage message)
        {
            client.ActiveCharacter.MovementEnded();
        }

        [WorldHandler(typeof (GameMapMovementCancelMessage))]
        public static void HandleGameMapMovementCancelMessage(WorldClient client, GameMapMovementCancelMessage message)
        {
            // todo : check if cell is available and if moving

            client.ActiveCharacter.StopMove(new ObjectPosition(client.ActiveCharacter.Map, (ushort) message.cellId,
                                                                client.ActiveCharacter.Position.Direction));
        }

        public static void SendGameContextCreateMessage(WorldClient client, byte context)
        {
            client.Send(new GameContextCreateMessage(context));
        }

        public static void SendGameContextDestroyMessage(WorldClient client)
        {
            client.Send(new GameContextDestroyMessage());
        }

        public static void SendGameMapChangeOrientationMessage(WorldClient client, Entity entity)
        {
            client.Send(
                new GameMapChangeOrientationMessage(new ActorOrientation((int) entity.Id,
                                                                         (uint) entity.Position.Direction)));
        }

        public static void SendGameContextRemoveElementMessage(WorldClient client, Entity entity)
        {
            client.Send(new GameContextRemoveElementMessage((int) entity.Id));
        }

        public static void SendGameContextRefreshEntityLookMessage(WorldClient client, Entity entity)
        {
            client.Send(new GameContextRefreshEntityLookMessage((int) entity.Id, entity.Look.EntityLook));
        }

        public static void SendGameMapMovementMessage(WorldClient client, List<uint> movementsKey, Entity entity)
        {
            client.Send(new GameMapMovementMessage(movementsKey, (int) entity.Id));
        }

        public static void SendGameEntitiesDispositionMessage(WorldClient client,
                                                              IEnumerable<ILocableIdentified> entities)
        {
            client.Send(
                new GameEntitiesDispositionMessage(
                    entities.Select(entry => entry.GetIdentifiedEntityDisposition()).ToList()));
        }
    }
}