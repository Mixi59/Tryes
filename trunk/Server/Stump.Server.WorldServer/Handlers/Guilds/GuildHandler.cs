﻿using System.Linq;
using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.Core.Extensions;
using Stump.Server.BaseServer.Network;
using Stump.Server.WorldServer.Core.Network;
using Stump.Server.WorldServer.Game;
using Stump.Server.WorldServer.Game.Guilds;
using Stump.Server.WorldServer.Handlers.Dialogs;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using GuildMember = Stump.Server.WorldServer.Game.Guilds.GuildMember;

namespace Stump.Server.WorldServer.Handlers.Guilds
{
    public class GuildHandler : WorldHandlerContainer
    {
        [WorldHandler(GuildGetInformationsMessage.Id)]
        public static void HandleGuildGetInformationsMessage(WorldClient client, GuildGetInformationsMessage message)
        {
            if (client.Character.Guild == null)
                return;

            switch (message.infoType)
            {
                case (sbyte)GuildInformationsTypeEnum.INFO_GENERAL:
                    SendGuildInformationsGeneralMessage(client, client.Character.Guild);
                    break;
                case (sbyte)GuildInformationsTypeEnum.INFO_MEMBERS:
                    SendGuildInformationsMembersMessage(client, client.Character.Guild);
                    break;
            }
        }

        [WorldHandler(GuildCreationValidMessage.Id)]
        public static void HandleGuildCreationValidMessage(WorldClient client, GuildCreationValidMessage message)
        {
            if (client.Character.Guild != null)
            {
                SendGuildCreationResultMessage(client, GuildCreationResultEnum.GUILD_CREATE_ERROR_ALREADY_IN_GUILD);
                return;
            }

            var result = Singleton<GuildManager>.Instance.CreateGuild(client.Character, message.guildName, message.guildEmblem);
            SendGuildCreationResultMessage(client, result);
        }

        [WorldHandler(GuildChangeMemberParametersMessage.Id)]
        public static void HandleGuildChangeMemberParametersMessage(WorldClient client, GuildChangeMemberParametersMessage message)
        {
            if (client.Character.Guild == null)
                return;

            var target = client.Character.Guild.TryGetMember(message.memberId);
            if (target == null)
                return;

            client.Character.Guild.ChangeParameters(client.Character, target, message.rank,
                                                    (byte) message.experienceGivenPercent, message.rights);
        }

        [WorldHandler(GuildKickRequestMessage.Id)]
        public static void HandleGuildKickRequestMessage(WorldClient client, GuildKickRequestMessage message)
        {
            if (client.Character.Guild == null)
                return;

            var target = client.Character.Guild.TryGetMember(message.kickedId);
            if (target == null)
                return;

            target.Guild.KickMember(client.Character, target);
        }

        [WorldHandler(GuildInvitationMessage.Id)]
        public static void HandleGuildInvitationMessage(WorldClient client, GuildInvitationMessage message)
        {
            if (client.Character.Guild == null)
                return;

            if (!client.Character.GuildMember.HasRight(GuildRightsBitEnum.GUILD_RIGHT_INVITE_NEW_MEMBERS))
            {
                // Vous n'avez pas le droit requis pour inviter des joueurs dans votre guilde.
                client.Character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 207);
                return;
            }

            var target = Singleton<World>.Instance.GetCharacter(message.targetId);
            if (target == null)
            {
                // Impossible d'inviter, ce joueur est inconnu ou non connecté.
                client.Character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 208);
                return;
            }

            if (target.Guild != null)
            {
                // Impossible, ce joueur est déjà dans une guilde
                client.Character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 206);
                return;
            }

            if (target.IsBusy())
            {
                // Ce joueur est occupé. Impossible de l'inviter.                    
                client.Character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 209);
                return;
            }

            var request = new GuildInvitationRequest(client.Character, target);
            request.Open();
        }

        [WorldHandler(GuildInvitationByNameMessage.Id)]
        public static void HandleGuildInvitationByNameMessage(WorldClient client, GuildInvitationByNameMessage message)
        {
            if (client.Character.Guild == null)
                return;

            if (!client.Character.GuildMember.HasRight(GuildRightsBitEnum.GUILD_RIGHT_INVITE_NEW_MEMBERS))
            {
                // Vous n'avez pas le droit requis pour inviter des joueurs dans votre guilde.
                client.Character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 207);
                return;
            }

            var target = Singleton<World>.Instance.GetCharacter(message.name);
            if (target == null)
            {
                // Impossible d'inviter, ce joueur est inconnu ou non connecté.
                client.Character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 208);
                return;
            }

            if (target.Guild != null)
            {
                // Impossible, ce joueur est déjà dans une guilde
                client.Character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 206);
                return;
            }

            if (target.IsBusy())
            {
                // Ce joueur est occupé. Impossible de l'inviter.                    
                client.Character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 209);
                return;
            }

            var request = new GuildInvitationRequest(client.Character, target);
            request.Open();
        }

        [WorldHandler(GuildInvitationAnswerMessage.Id)]
        public static void HandleGuildInvitationAnswerMessage(WorldClient client, GuildInvitationAnswerMessage message)
        {
            var request = client.Character.RequestBox as GuildInvitationRequest;

            if (request == null)
                return;

            if (client.Character == request.Source && !message.accept)
                request.Cancel();
            else if (client.Character == request.Target)
            {
                if (message.accept)
                    request.Accept();
                else
                    request.Deny();
            }
        }

        [WorldHandler(GuildMemberSetWarnOnConnectionMessage.Id)]
        public static void HandleGuildMemberSetWarnOnConnectionMessage(WorldClient client, GuildMemberSetWarnOnConnectionMessage message)
        {
            client.Character.WarnOnGuildConnection = message.enable;
        }

        public static void SendGuildMemberWarnOnConnectionStateMessage(IPacketReceiver client, bool state)
        {
            client.Send(new GuildMemberWarnOnConnectionStateMessage(state));
        }

        public static void SendGuildInvitedMessage(IPacketReceiver client, Character recruter)
        {
            client.Send(new GuildInvitedMessage(recruter.Id, recruter.Name, recruter.Guild.GetBasicGuildInformations()));
        }

        public static void SendGuildInvitationStateRecruterMessage(IPacketReceiver client, Character recruted, GuildInvitationStateEnum state)
        {
            client.Send(new GuildInvitationStateRecruterMessage(recruted.Name, (sbyte)state));
        }

        public static void SendGuildLeftMessage(IPacketReceiver client)
        {
            client.Send(new GuildLeftMessage());
        }

        public static void SendGuildCreationResultMessage(IPacketReceiver client, GuildCreationResultEnum result)
        {
            client.Send(new GuildCreationResultMessage((sbyte)result));
        }

        public static void SendGuildMembershipMessage(IPacketReceiver client, GuildMember member)
        {
            client.Send(new GuildMembershipMessage(member.Guild.GetGuildInformations(), (uint)member.Rights, true));
        }

        public static void SendGuildInformationsGeneralMessage(IPacketReceiver client, Guild guild)
        {
            client.Send(new GuildInformationsGeneralMessage(true, false, guild.Level, guild.ExperienceLevelFloor, guild.Experience, guild.ExperienceNextLevelFloor, guild.CreationDate.GetUnixTimeStamp())); 
        }

        public static void SendGuildInformationsMembersMessage(IPacketReceiver client, Guild guild)
        {
            client.Send(new GuildInformationsMembersMessage(guild.Members.Select(x => x.GetNetworkGuildMember())));
        }

        public static void SendGuildInformationsMemberUpdateMessage(IPacketReceiver client, GuildMember member)
        {
            client.Send(new GuildInformationsMemberUpdateMessage(member.GetNetworkGuildMember()));
        }

        public static void SendGuildJoinedMessage(IPacketReceiver client, GuildMember member)
        {
            client.Send(new GuildJoinedMessage(member.Guild.GetGuildInformations(), (uint)member.Rights, true));
        }

        public static void SendGuildMemberLeavingMessage(IPacketReceiver client, GuildMember member, bool kicked)
        {
            client.Send(new GuildMemberLeavingMessage(kicked, member.Id));
        }
    }
}