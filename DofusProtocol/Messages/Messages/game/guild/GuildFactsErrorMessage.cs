

// Generated on 12/29/2014 21:13:10
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class GuildFactsErrorMessage : Message
    {
        public const uint Id = 6424;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public int guildId;
        
        public GuildFactsErrorMessage()
        {
        }
        
        public GuildFactsErrorMessage(int guildId)
        {
            this.guildId = guildId;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(guildId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            guildId = reader.ReadInt();
            if (guildId < 0)
                throw new Exception("Forbidden value on guildId = " + guildId + ", it doesn't respect the following condition : guildId < 0");
        }
        
        public override int GetSerializationSize()
        {
            return sizeof(int);
        }
        
    }
    
}