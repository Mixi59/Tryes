
// Generated on 03/25/2013 19:24:04
using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class CharacterReplayWithRelookRequestMessage : CharacterReplayRequestMessage
    {
        public const uint Id = 6354;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public int cosmeticId;
        
        public CharacterReplayWithRelookRequestMessage()
        {
        }
        
        public CharacterReplayWithRelookRequestMessage(int characterId, int cosmeticId)
         : base(characterId)
        {
            this.cosmeticId = cosmeticId;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteInt(cosmeticId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            cosmeticId = reader.ReadInt();
            if (cosmeticId < 0)
                throw new Exception("Forbidden value on cosmeticId = " + cosmeticId + ", it doesn't respect the following condition : cosmeticId < 0");
        }
        
    }
    
}