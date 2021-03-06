

// Generated on 10/30/2016 16:20:46
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class LivingObjectDissociateMessage : Message
    {
        public const uint Id = 5723;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public int livingUID;
        public byte livingPosition;
        
        public LivingObjectDissociateMessage()
        {
        }
        
        public LivingObjectDissociateMessage(int livingUID, byte livingPosition)
        {
            this.livingUID = livingUID;
            this.livingPosition = livingPosition;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarInt(livingUID);
            writer.WriteByte(livingPosition);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            livingUID = reader.ReadVarInt();
            if (livingUID < 0)
                throw new Exception("Forbidden value on livingUID = " + livingUID + ", it doesn't respect the following condition : livingUID < 0");
            livingPosition = reader.ReadByte();
            if (livingPosition < 0 || livingPosition > 255)
                throw new Exception("Forbidden value on livingPosition = " + livingPosition + ", it doesn't respect the following condition : livingPosition < 0 || livingPosition > 255");
        }
        
    }
    
}