using System;
using System.Threading;
using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Commands;
using Stump.Server.WorldServer.Core.Network;
using Stump.Server.WorldServer.Worlds.Accounts;
using Stump.Server.WorldServer.Worlds.Actors.RolePlay.Characters;

namespace Stump.Server.WorldServer.Commands.Commands
{
    public class BanCommand : CommandBase
    {
        public BanCommand()
        {
            Aliases = new[] { "ban" };
            RequiredRole = RoleEnum.Administrator;
            Description = "Ban a player";

            AddParameter("target", "t", "Player to ban", converter: ParametersConverter.CharacterConverter);
            AddParameter<int>("time", "time", "Ban duration (in minutes)", isOptional: true);
            AddParameter("reason", "r", "Reason of ban", "No reason");
            AddParameter<bool>("life", "l", "Specify a life ban", isOptional: true);
        }

        public override void Execute(TriggerBase trigger)
        {
            var target = trigger.Get<Character>("target");
            var reason = trigger.Get<string>("reason");

            if (target == null)
            {
                trigger.ReplyError("Define a target !");
                return;
            }

            if (trigger.IsArgumentDefined("time"))
            {
                var time = trigger.Get<int>("time");
                var source = trigger.GetSource() as WorldClient;

                if (source != null)
                    AccountManager.Instance.BanLater(target.Client.Account, source.Account, TimeSpan.FromMinutes(time), reason);
                else
                    AccountManager.Instance.BanLater(target.Client.Account, TimeSpan.FromMinutes(time), reason);
            }
            else if (trigger.IsArgumentDefined("life"))
            {
                var source = trigger.GetSource() as WorldClient;

                if (source != null)
                    AccountManager.Instance.BanLater(target.Client.Account, source.Account, TimeSpan.MaxValue, reason);
                else
                    AccountManager.Instance.BanLater(target.Client.Account, TimeSpan.MaxValue, reason);
            }

            target.Client.Disconnect();
        }
    }
}