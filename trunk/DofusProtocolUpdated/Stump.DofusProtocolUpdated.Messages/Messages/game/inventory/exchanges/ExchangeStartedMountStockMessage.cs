

// Generated on 12/12/2013 16:57:18
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class ExchangeStartedMountStockMessage : Message
    {
        public const uint Id = 5984;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public IEnumerable<Types.ObjectItem> objectsInfos;
        
        public ExchangeStartedMountStockMessage()
        {
        }
        
        public ExchangeStartedMountStockMessage(IEnumerable<Types.ObjectItem> objectsInfos)
        {
            this.objectsInfos = objectsInfos;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUShort((ushort)objectsInfos.Count());
            foreach (var entry in objectsInfos)
            {
                 entry.Serialize(writer);
            }
        }
        
        public override void Deserialize(IDataReader reader)
        {
            var limit = reader.ReadUShort();
            objectsInfos = new Types.ObjectItem[limit];
            for (int i = 0; i < limit; i++)
            {
                 (objectsInfos as Types.ObjectItem[])[i] = new Types.ObjectItem();
                 (objectsInfos as Types.ObjectItem[])[i].Deserialize(reader);
            }
        }
        
        public override int GetSerializationSize()
        {
            return sizeof(short) + objectsInfos.Sum(x => x.GetSerializationSize());
        }
        
    }
    
}