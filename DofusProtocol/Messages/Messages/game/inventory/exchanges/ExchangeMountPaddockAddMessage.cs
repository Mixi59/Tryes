

// Generated on 02/11/2015 10:20:37
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class ExchangeMountPaddockAddMessage : Message
    {
        public const uint Id = 6049;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public Types.MountClientData mountDescription;
        
        public ExchangeMountPaddockAddMessage()
        {
        }
        
        public ExchangeMountPaddockAddMessage(Types.MountClientData mountDescription)
        {
            this.mountDescription = mountDescription;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            mountDescription.Serialize(writer);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            mountDescription = new Types.MountClientData();
            mountDescription.Deserialize(reader);
        }
        
    }
    
}