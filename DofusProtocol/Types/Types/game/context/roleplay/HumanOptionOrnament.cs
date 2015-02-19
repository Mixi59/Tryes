

// Generated on 02/18/2015 11:05:52
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
    public class HumanOptionOrnament : HumanOption
    {
        public const short Id = 411;
        public override short TypeId
        {
            get { return Id; }
        }
        
        public short ornamentId;
        
        public HumanOptionOrnament()
        {
        }
        
        public HumanOptionOrnament(short ornamentId)
        {
            this.ornamentId = ornamentId;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarShort(ornamentId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            ornamentId = reader.ReadVarShort();
            if (ornamentId < 0)
                throw new Exception("Forbidden value on ornamentId = " + ornamentId + ", it doesn't respect the following condition : ornamentId < 0");
        }
        
        
    }
    
}