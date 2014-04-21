

// Generated on 03/02/2014 20:42:30
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class AchievementDetailedListRequestMessage : Message
    {
        public const uint Id = 6357;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public short categoryId;
        
        public AchievementDetailedListRequestMessage()
        {
        }
        
        public AchievementDetailedListRequestMessage(short categoryId)
        {
            this.categoryId = categoryId;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(categoryId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            categoryId = reader.ReadShort();
            if (categoryId < 0)
                throw new Exception("Forbidden value on categoryId = " + categoryId + ", it doesn't respect the following condition : categoryId < 0");
        }
        
        public override int GetSerializationSize()
        {
            return sizeof(short);
        }
        
    }
    
}