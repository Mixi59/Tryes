﻿using System;
using System.Collections.Generic;
using System.Linq;
using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Database;
using Stump.DofusProtocol.Types;
using Stump.Server.WorldServer.Database.Guilds;
using Stump.Server.WorldServer.Database.Characters;
using Stump.Server.WorldServer.Database.Accounts;
using Stump.Server.WorldServer.Game.Accounts;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;

namespace Stump.Server.WorldServer.Game.Guilds
{
    public class GuildManager : DataManager<GuildManager>
    {
        /// <summary>
        /// Returns null if not found
        /// </summary>
        /// <param name="guildId"></param>
        /// <returns></returns>
        public GuildRecord FindById(int guildId)
        {
            return Guild.Instance.FindGuildByGuildId(guildId);
        }

        public int FindGuildIdByCharacter(Character character)
        {
            var guildMember = Guild.Instance.FindGuildMemberByCharacterId(character.Id);

            return guildMember == null ? 0 : guildMember.GuildId;
        }

        public List<GuildMember> GetGuildMembers(int guildId)
        {
            var members = Guild.Instance.GuildMembers.Where(gMembers => gMembers.GuildId == guildId);

            var guildMembers = new List<GuildMember>();

            foreach (var member in members)
            {
                var character = World.Instance.GetCharacter(member.CharacterId);
                GuildMember guildMember;

                if (character != null)
                {
                    guildMember = new GuildMember(
                        member.CharacterId,
                        ExperienceManager.Instance.GetCharacterLevel(character.Experience),
                        character.Name,
                        (sbyte)character.Breed.Id,
                        character.Sex == SexTypeEnum.SEX_FEMALE,
                        member.RankId,
                        member.GivenExperience,
                        member.GivenPercent,
                        (uint)member.Rights, //Rights
                        1, //Connected
                        (sbyte) character.AlignmentSide,
                        0, //Hours since last connection
                        0, //Mood SmileyId(None = 0)
                        member.AccountId,
                        0); //AchievementPoints
                }
                else
                {
                    var characterRecord = Database.FirstOrDefault<CharacterRecord>(string.Format(GuildMemberRelator.FetchCharacterById, member.CharacterId));

                    guildMember = new GuildMember(
                        member.CharacterId,
                        ExperienceManager.Instance.GetCharacterLevel(characterRecord.Experience),
                        characterRecord.Name,
                        (sbyte)characterRecord.Breed,
                        characterRecord.Sex == SexTypeEnum.SEX_FEMALE,
                        member.RankId,
                        member.GivenExperience,
                        member.GivenPercent,
                        (uint)member.Rights, //Rights
                        0, //Disconnected
                        (sbyte)characterRecord.AlignmentSide,
                        1, //Hours since last connection
                        0, //Mood SmileyId
                        member.AccountId,
                        0); //AchievementPoints   
                }

                guildMembers.Add(guildMember);
            }

            return guildMembers;
        }

        public GuildMemberRecord ChangeMemberParameters(int guildId, Character character, int targetId, short rank, sbyte xpPercent, uint rights)
        {
            var guildMember = Guild.Instance.FindGuildMemberByCharacterId(targetId);
            var clientRights = Guild.Instance.FindGuildMemberByCharacterId(character.Id).Rights;

            if (guildMember == null)
                return null;
            if (guildMember.GuildId != character.GuildId)
                return null;

            if (GuildMemberHasRight(GuildRightsBitEnum.GUILD_RIGHT_MANAGE_RANKS, clientRights))
                guildMember.RankId = rank;
            if (GuildMemberHasRight(GuildRightsBitEnum.GUILD_RIGHT_MANAGE_XP_CONTRIBUTION, clientRights) || (character.Id == targetId && GuildMemberHasRight(GuildRightsBitEnum.GUILD_RIGHT_MANAGE_MY_XP_CONTRIBUTION, clientRights)))
                guildMember.GivenPercent = xpPercent;
            if (GuildMemberHasRight(GuildRightsBitEnum.GUILD_RIGHT_MANAGE_RIGHTS, clientRights))
                guildMember.Rights = rights;

            var database = WorldServer.Instance.DBAccessor.Database;
            database.Save(guildMember);

            return guildMember;
        }

        public bool KickMember(int guildId, Character character, int targetId)
        {
            var guildMember = Guild.Instance.FindGuildMemberByCharacterId(targetId);

            if (guildMember == null)
                return false;
            if (guildMember.GuildId != character.GuildId)
                return false;

            var clientRights = Guild.Instance.FindGuildMemberByCharacterId(character.Id).Rights;

            if (!GuildMemberHasRight(GuildRightsBitEnum.GUILD_RIGHT_BAN_MEMBERS, clientRights))
                return false;

            if (Guild.Instance.KickMember(targetId))
                WorldServer.Instance.DBAccessor.Database.Delete(guildMember);
            else
                return false;

            return true;
        }

        public bool GuildMemberHasRight(GuildRightsBitEnum right, uint rights)
        {
            var gRights = (GuildRightsBitEnum)rights;

            return gRights.HasFlag(right);
        }
    }
}
