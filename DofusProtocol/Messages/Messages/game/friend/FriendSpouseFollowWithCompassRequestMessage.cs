

// Generated on 04/24/2015 03:38:08
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class FriendSpouseFollowWithCompassRequestMessage : Message
    {
        public const uint Id = 5606;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public bool enable;
        
        public FriendSpouseFollowWithCompassRequestMessage()
        {
        }
        
        public FriendSpouseFollowWithCompassRequestMessage(bool enable)
        {
            this.enable = enable;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(enable);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            enable = reader.ReadBoolean();
        }
        
    }
    
}