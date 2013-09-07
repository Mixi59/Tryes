﻿using System.Collections.Generic;
using System.Linq;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.DofusProtocol.Types;
using Stump.Core.Extensions;
using Stump.Server.BaseServer.Network;
using Stump.Server.WorldServer.Core.Network;
using Stump.Server.WorldServer.Database.Guilds;
using Stump.Server.WorldServer.Game.Guilds;
using Stump.Server.WorldServer.Game;

namespace Stump.Server.WorldServer.Handlers.Guilds
{
    public class GuildHandler : WorldHandlerContainer
    {
        [WorldHandler(GuildGetInformationsMessage.Id)]
        public static void HandleGuildGetInformationsMessage(WorldClient client, GuildGetInformationsMessage message)
        {
            var guildId = client.Character.GuildId;
            if (guildId == 0) return;

            switch (message.infoType)
            {
                case (sbyte)GuildInformationsTypeEnum.INFO_GENERAL:
                    SendGuildInformationsGeneralMessage(client);
                    break;
                case (sbyte)GuildInformationsTypeEnum.INFO_MEMBERS:
                    client.Send(new GuildInformationsMembersMessage(GuildManager.Instance.GetGuildMembers(guildId)));
                    break;
            }
        }

        [WorldHandler(GuildChangeMemberParametersMessage.Id)]
        public static void HandleGuildChangeMemberParametersMessage(WorldClient client, GuildChangeMemberParametersMessage message)
        {
            var guildId = client.Character.GuildId;
            if (guildId == 0) return;

            var member = GuildManager.Instance.ChangeMemberParameters(guildId, client.Character, message.memberId, message.rank, message.experienceGivenPercent, message.rights);

            if (member != null)
                client.Send(new GuildInformationsMembersMessage(GuildManager.Instance.GetGuildMembers(guildId)));
                //client.Send(new GuildInformationsMemberUpdateMessage(member)); //Best to use
        }

        public static void SendGuildMembershipMessage(WorldClient client)
        {
            var guildId = client.Character.GuildId;
            if (guildId == 0) return;

            var guildInfo = GuildManager.Instance.FindById(guildId);
            var guildMember = Guild.Instance.FindGuildMemberByCharacterId(client.Character.Id);

            client.Send(new GuildMembershipMessage(new GuildInformations(guildId, guildInfo.Name, guildInfo.GuildEmblem), (uint)guildMember.Rights, true));
        }

        public static void SendGuildInformationsGeneralMessage(WorldClient client)
        {
            var guildId = client.Character.GuildId;
            if (guildId == 0) return;

            var guildInfo = GuildManager.Instance.FindById(guildId);

            client.Send(new GuildInformationsGeneralMessage(true, false, (byte)guildInfo.Level, 10, guildInfo.Experience, 100, guildInfo.CreationDate.GetUnixTimeStamp()));
        }
    }
}
