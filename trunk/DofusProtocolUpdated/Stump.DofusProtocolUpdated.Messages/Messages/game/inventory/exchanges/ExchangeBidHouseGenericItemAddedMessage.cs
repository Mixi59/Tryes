

// Generated on 12/12/2013 16:57:14
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class ExchangeBidHouseGenericItemAddedMessage : Message
    {
        public const uint Id = 5947;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public int objGenericId;
        
        public ExchangeBidHouseGenericItemAddedMessage()
        {
        }
        
        public ExchangeBidHouseGenericItemAddedMessage(int objGenericId)
        {
            this.objGenericId = objGenericId;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(objGenericId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            objGenericId = reader.ReadInt();
        }
        
        public override int GetSerializationSize()
        {
            return sizeof(int);
        }
        
    }
    
}