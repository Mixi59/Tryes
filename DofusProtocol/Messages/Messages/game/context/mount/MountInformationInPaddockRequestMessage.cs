

// Generated on 02/11/2015 10:20:29
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class MountInformationInPaddockRequestMessage : Message
    {
        public const uint Id = 5975;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public int mapRideId;
        
        public MountInformationInPaddockRequestMessage()
        {
        }
        
        public MountInformationInPaddockRequestMessage(int mapRideId)
        {
            this.mapRideId = mapRideId;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(mapRideId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            mapRideId = reader.ReadInt();
        }
        
    }
    
}