

// Generated on 10/30/2016 16:20:37
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class LeaveDialogRequestMessage : Message
    {
        public const uint Id = 5501;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        
        public LeaveDialogRequestMessage()
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