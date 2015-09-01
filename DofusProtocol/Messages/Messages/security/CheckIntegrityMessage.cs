

// Generated on 09/01/2015 10:48:31
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class CheckIntegrityMessage : Message
    {
        public const uint Id = 6372;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public IEnumerable<sbyte> data;
        
        public CheckIntegrityMessage()
        {
        }
        
        public CheckIntegrityMessage(IEnumerable<sbyte> data)
        {
            this.data = data;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            var data_before = writer.Position;
            var data_count = 0;
            writer.WriteVarInt(0);
            foreach (var entry in data)
            {
                writer.WriteSByte(entry);
                data_count++;
            }
            var data_after = writer.Position;
            writer.Seek((int)data_before);
            writer.WriteVarInt((int)data_count);
            writer.Seek((int)data_after);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            var limit = reader.ReadVarInt();
            var data_ = new sbyte[limit];
            for (int i = 0; i < limit; i++)
            {
                 data_[i] = reader.ReadSByte();
            }
            data = data_;
        }
        
    }
    
}