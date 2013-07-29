

// Generated on 07/29/2013 23:07:58
using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class HouseGuildRightsMessage : Message
    {
        public const uint Id = 5703;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public short houseId;
        public Types.GuildInformations guildInfo;
        public uint rights;
        
        public HouseGuildRightsMessage()
        {
        }
        
        public HouseGuildRightsMessage(short houseId, Types.GuildInformations guildInfo, uint rights)
        {
            this.houseId = houseId;
            this.guildInfo = guildInfo;
            this.rights = rights;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(houseId);
            guildInfo.Serialize(writer);
            writer.WriteUInt(rights);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            houseId = reader.ReadShort();
            if (houseId < 0)
                throw new Exception("Forbidden value on houseId = " + houseId + ", it doesn't respect the following condition : houseId < 0");
            guildInfo = new Types.GuildInformations();
            guildInfo.Deserialize(reader);
            rights = reader.ReadUInt();
            if (rights < 0 || rights > 4294967295)
                throw new Exception("Forbidden value on rights = " + rights + ", it doesn't respect the following condition : rights < 0 || rights > 4294967295");
        }
        
        public override int GetSerializationSize()
        {
            return sizeof(short) + guildInfo.GetSerializationSize() + sizeof(uint);
        }
        
    }
    
}