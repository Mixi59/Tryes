﻿
using System;
using Stump.DofusProtocol.Messages;
using Stump.Tools.Proxy.Data;
using Stump.Tools.Proxy.Network;

namespace Stump.Tools.Proxy.Handlers.World
{
    public class TeleportHandler : WorldHandlerContainer
    {
        [WorldHandler(typeof (CurrentMapMessage))]
        public static void HandleCurrentMapMessage(WorldClient client, CurrentMapMessage message)
        {
            client.Send(message);

            if (client.HasReceive(typeof(LeaveDialogMessage), 2))
                client.GuessNpcReply = client.LastNpcReply;

            if(client.GuessAction)
            {
                client.CallWhenTeleported(() => DataFactory.BuildActionTeleport(client, message));
            }
        }

        [WorldHandler(typeof (TeleportDestinationsListMessage))]
        public static void HandleTeleportDestinationsListMessage(WorldClient client, TeleportDestinationsListMessage message)
        {
            client.Send(message);
        }
    }
}