

// Generated on 03/02/2014 20:42:45
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class SpellUpgradeRequestMessage : Message
    {
        public const uint Id = 5608;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public short spellId;
        
        public SpellUpgradeRequestMessage()
        {
        }
        
        public SpellUpgradeRequestMessage(short spellId)
        {
            this.spellId = spellId;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(spellId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            spellId = reader.ReadShort();
            if (spellId < 0)
                throw new Exception("Forbidden value on spellId = " + spellId + ", it doesn't respect the following condition : spellId < 0");
        }
        
        public override int GetSerializationSize()
        {
            return sizeof(short);
        }
        
    }
    
}