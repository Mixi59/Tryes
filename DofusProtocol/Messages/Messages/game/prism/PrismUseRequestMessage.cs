

// Generated on 02/18/2015 10:46:29
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class PrismUseRequestMessage : Message
    {
        public const uint Id = 6041;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        
        public PrismUseRequestMessage()
        {
        }
        
        
        public override void Serialize(IDataWriter writer)
        {
        }
        
        public override void Deserialize(IDataReader reader)
        {
        }
        
    }
    
}