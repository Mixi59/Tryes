

// Generated on 10/30/2016 16:20:38
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class GuildGetInformationsMessage : Message
    {
        public const uint Id = 5550;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public sbyte infoType;
        
        public GuildGetInformationsMessage()
        {
        }
        
        public GuildGetInformationsMessage(sbyte infoType)
        {
            this.infoType = infoType;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(infoType);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            infoType = reader.ReadSByte();
            if (infoType < 0)
                throw new Exception("Forbidden value on infoType = " + infoType + ", it doesn't respect the following condition : infoType < 0");
        }
        
    }
    
}