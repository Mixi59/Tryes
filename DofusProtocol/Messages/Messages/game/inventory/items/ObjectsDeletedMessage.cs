

// Generated on 10/30/2016 16:20:47
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class ObjectsDeletedMessage : Message
    {
        public const uint Id = 6034;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public IEnumerable<int> objectUID;
        
        public ObjectsDeletedMessage()
        {
        }
        
        public ObjectsDeletedMessage(IEnumerable<int> objectUID)
        {
            this.objectUID = objectUID;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            var objectUID_before = writer.Position;
            var objectUID_count = 0;
            writer.WriteUShort(0);
            foreach (var entry in objectUID)
            {
                 writer.WriteVarInt(entry);
                 objectUID_count++;
            }
            var objectUID_after = writer.Position;
            writer.Seek((int)objectUID_before);
            writer.WriteUShort((ushort)objectUID_count);
            writer.Seek((int)objectUID_after);

        }
        
        public override void Deserialize(IDataReader reader)
        {
            var limit = reader.ReadUShort();
            var objectUID_ = new int[limit];
            for (int i = 0; i < limit; i++)
            {
                 objectUID_[i] = reader.ReadVarInt();
            }
            objectUID = objectUID_;
        }
        
    }
    
}