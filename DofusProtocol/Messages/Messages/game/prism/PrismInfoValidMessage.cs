

// Generated on 03/02/2014 20:42:56
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class PrismInfoValidMessage : Message
    {
        public const uint Id = 5858;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public Types.ProtectedEntityWaitingForHelpInfo waitingForHelpInfo;
        
        public PrismInfoValidMessage()
        {
        }
        
        public PrismInfoValidMessage(Types.ProtectedEntityWaitingForHelpInfo waitingForHelpInfo)
        {
            this.waitingForHelpInfo = waitingForHelpInfo;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            waitingForHelpInfo.Serialize(writer);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            waitingForHelpInfo = new Types.ProtectedEntityWaitingForHelpInfo();
            waitingForHelpInfo.Deserialize(reader);
        }
        
        public override int GetSerializationSize()
        {
            return waitingForHelpInfo.GetSerializationSize();
        }
        
    }
    
}