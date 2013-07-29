

// Generated on 07/29/2013 23:08:18
using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class ExchangeBidHouseInListUpdatedMessage : ExchangeBidHouseInListAddedMessage
    {
        public const uint Id = 6337;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        
        public ExchangeBidHouseInListUpdatedMessage()
        {
        }
        
        public ExchangeBidHouseInListUpdatedMessage(int itemUID, int objGenericId, short powerRate, bool overMax, IEnumerable<Types.ObjectEffect> effects, IEnumerable<int> prices)
         : base(itemUID, objGenericId, powerRate, overMax, effects, prices)
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
        
        public override int GetSerializationSize()
        {
            return base.GetSerializationSize();
        }
        
    }
    
}