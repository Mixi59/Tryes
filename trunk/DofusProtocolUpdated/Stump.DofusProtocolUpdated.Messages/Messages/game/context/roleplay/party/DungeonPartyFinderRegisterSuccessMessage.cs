

// Generated on 12/12/2013 16:57:04
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class DungeonPartyFinderRegisterSuccessMessage : Message
    {
        public const uint Id = 6241;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public IEnumerable<short> dungeonIds;
        
        public DungeonPartyFinderRegisterSuccessMessage()
        {
        }
        
        public DungeonPartyFinderRegisterSuccessMessage(IEnumerable<short> dungeonIds)
        {
            this.dungeonIds = dungeonIds;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUShort((ushort)dungeonIds.Count());
            foreach (var entry in dungeonIds)
            {
                 writer.WriteShort(entry);
            }
        }
        
        public override void Deserialize(IDataReader reader)
        {
            var limit = reader.ReadUShort();
            dungeonIds = new short[limit];
            for (int i = 0; i < limit; i++)
            {
                 (dungeonIds as short[])[i] = reader.ReadShort();
            }
        }
        
        public override int GetSerializationSize()
        {
            return sizeof(short) + dungeonIds.Sum(x => sizeof(short));
        }
        
    }
    
}