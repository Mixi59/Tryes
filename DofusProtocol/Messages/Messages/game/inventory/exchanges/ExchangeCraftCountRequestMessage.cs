

// Generated on 10/30/2016 16:20:42
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class ExchangeCraftCountRequestMessage : Message
    {
        public const uint Id = 6597;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public int count;
        
        public ExchangeCraftCountRequestMessage()
        {
        }
        
        public ExchangeCraftCountRequestMessage(int count)
        {
            this.count = count;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarInt(count);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            count = reader.ReadVarInt();
        }
        
    }
    
}