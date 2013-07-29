

// Generated on 07/29/2013 23:07:28
using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class IdentificationFailedForBadVersionMessage : IdentificationFailedMessage
    {
        public const uint Id = 21;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public Types.Version requiredVersion;
        
        public IdentificationFailedForBadVersionMessage()
        {
        }
        
        public IdentificationFailedForBadVersionMessage(sbyte reason, Types.Version requiredVersion)
         : base(reason)
        {
            this.requiredVersion = requiredVersion;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            requiredVersion.Serialize(writer);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            requiredVersion = new Types.Version();
            requiredVersion.Deserialize(reader);
        }
        
        public override int GetSerializationSize()
        {
            return base.GetSerializationSize() + requiredVersion.GetSerializationSize();
        }
        
    }
    
}