

// Generated on 04/24/2015 03:37:58
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class CharacterReplayWithRemodelRequestMessage : CharacterReplayRequestMessage
    {
        public const uint Id = 6551;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public Types.RemodelingInformation remodel;
        
        public CharacterReplayWithRemodelRequestMessage()
        {
        }
        
        public CharacterReplayWithRemodelRequestMessage(int characterId, Types.RemodelingInformation remodel)
         : base(characterId)
        {
            this.remodel = remodel;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            remodel.Serialize(writer);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            remodel = new Types.RemodelingInformation();
            remodel.Deserialize(reader);
        }
        
    }
    
}