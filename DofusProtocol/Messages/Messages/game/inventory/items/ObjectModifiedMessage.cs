

// Generated on 10/30/2016 16:20:47
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class ObjectModifiedMessage : Message
    {
        public const uint Id = 3029;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public Types.ObjectItem @object;
        
        public ObjectModifiedMessage()
        {
        }
        
        public ObjectModifiedMessage(Types.ObjectItem @object)
        {
            this.@object = @object;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            @object.Serialize(writer);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            @object = new Types.ObjectItem();
            @object.Deserialize(reader);
        }
        
    }
    
}