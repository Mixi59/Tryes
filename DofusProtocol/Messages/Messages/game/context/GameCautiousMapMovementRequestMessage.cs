

// Generated on 10/30/2016 16:20:25
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class GameCautiousMapMovementRequestMessage : GameMapMovementRequestMessage
    {
        public const uint Id = 6496;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        
        public GameCautiousMapMovementRequestMessage()
        {
        }
        
        public GameCautiousMapMovementRequestMessage(IEnumerable<short> keyMovements, int mapId)
         : base(keyMovements, mapId)
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
        }
        
    }
    
}