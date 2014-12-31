

// Generated on 12/29/2014 21:14:39
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
    public class GuildMember : CharacterMinimalInformations
    {
        public const short Id = 88;
        public override short TypeId
        {
            get { return Id; }
        }
        
        public sbyte breed;
        public bool sex;
        public short rank;
        public long givenExperience;
        public sbyte experienceGivenPercent;
        public int rights;
        public sbyte connected;
        public sbyte alignmentSide;
        public ushort hoursSinceLastConnection;
        public sbyte moodSmileyId;
        public int accountId;
        public int achievementPoints;
        public Types.PlayerStatus status;
        
        public GuildMember()
        {
        }
        
        public GuildMember(int id, byte level, string name, sbyte breed, bool sex, short rank, long givenExperience, sbyte experienceGivenPercent, int rights, sbyte connected, sbyte alignmentSide, ushort hoursSinceLastConnection, sbyte moodSmileyId, int accountId, int achievementPoints, Types.PlayerStatus status)
         : base(id, level, name)
        {
            this.breed = breed;
            this.sex = sex;
            this.rank = rank;
            this.givenExperience = givenExperience;
            this.experienceGivenPercent = experienceGivenPercent;
            this.rights = rights;
            this.connected = connected;
            this.alignmentSide = alignmentSide;
            this.hoursSinceLastConnection = hoursSinceLastConnection;
            this.moodSmileyId = moodSmileyId;
            this.accountId = accountId;
            this.achievementPoints = achievementPoints;
            this.status = status;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteSByte(breed);
            writer.WriteBoolean(sex);
            writer.WriteShort(rank);
            writer.WriteLong(givenExperience);
            writer.WriteSByte(experienceGivenPercent);
            writer.WriteInt(rights);
            writer.WriteSByte(connected);
            writer.WriteSByte(alignmentSide);
            writer.WriteUShort(hoursSinceLastConnection);
            writer.WriteSByte(moodSmileyId);
            writer.WriteInt(accountId);
            writer.WriteInt(achievementPoints);
            writer.WriteShort(status.TypeId);
            status.Serialize(writer);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            breed = reader.ReadSByte();
            sex = reader.ReadBoolean();
            rank = reader.ReadShort();
            if (rank < 0)
                throw new Exception("Forbidden value on rank = " + rank + ", it doesn't respect the following condition : rank < 0");
            givenExperience = reader.ReadLong();
            if (givenExperience < 0 || givenExperience > 9.007199254740992E15)
                throw new Exception("Forbidden value on givenExperience = " + givenExperience + ", it doesn't respect the following condition : givenExperience < 0 || givenExperience > 9.007199254740992E15");
            experienceGivenPercent = reader.ReadSByte();
            if (experienceGivenPercent < 0 || experienceGivenPercent > 100)
                throw new Exception("Forbidden value on experienceGivenPercent = " + experienceGivenPercent + ", it doesn't respect the following condition : experienceGivenPercent < 0 || experienceGivenPercent > 100");
            rights = reader.ReadInt();
            if (rights < 0)
                throw new Exception("Forbidden value on rights = " + rights + ", it doesn't respect the following condition : rights < 0");
            connected = reader.ReadSByte();
            if (connected < 0)
                throw new Exception("Forbidden value on connected = " + connected + ", it doesn't respect the following condition : connected < 0");
            alignmentSide = reader.ReadSByte();
            hoursSinceLastConnection = reader.ReadUShort();
            if (hoursSinceLastConnection < 0 || hoursSinceLastConnection > 65535)
                throw new Exception("Forbidden value on hoursSinceLastConnection = " + hoursSinceLastConnection + ", it doesn't respect the following condition : hoursSinceLastConnection < 0 || hoursSinceLastConnection > 65535");
            moodSmileyId = reader.ReadSByte();
            accountId = reader.ReadInt();
            if (accountId < 0)
                throw new Exception("Forbidden value on accountId = " + accountId + ", it doesn't respect the following condition : accountId < 0");
            achievementPoints = reader.ReadInt();
            status = Types.ProtocolTypeManager.GetInstance<Types.PlayerStatus>(reader.ReadShort());
            status.Deserialize(reader);
        }
        
        public override int GetSerializationSize()
        {
            return base.GetSerializationSize() + sizeof(sbyte) + sizeof(bool) + sizeof(short) + sizeof(long) + sizeof(sbyte) + sizeof(int) + sizeof(sbyte) + sizeof(sbyte) + sizeof(ushort) + sizeof(sbyte) + sizeof(int) + sizeof(int) + sizeof(short) + status.GetSerializationSize();
        }
        
    }
    
}