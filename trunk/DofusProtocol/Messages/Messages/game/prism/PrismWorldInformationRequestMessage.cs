

// Generated on 03/02/2014 20:42:56
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class PrismWorldInformationRequestMessage : Message
    {
        public const uint Id = 5985;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public bool join;
        
        public PrismWorldInformationRequestMessage()
        {
        }
        
        public PrismWorldInformationRequestMessage(bool join)
        {
            this.join = join;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(join);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            join = reader.ReadBoolean();
        }
        
        public override int GetSerializationSize()
        {
            return sizeof(bool);
        }
        
    }
    
}