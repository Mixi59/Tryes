// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ExchangeBidHouseInListRemovedMessage.xml' the '24/01/2012 22:50:48'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ExchangeBidHouseInListRemovedMessage : Message
	{
		public const uint Id = 5950;
		public override uint MessageId
		{
			get
			{
				return 5950;
			}
		}
		
		public int itemUID;
		
		public ExchangeBidHouseInListRemovedMessage()
		{
		}
		
		public ExchangeBidHouseInListRemovedMessage(int itemUID)
		{
			this.itemUID = itemUID;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteInt(itemUID);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			itemUID = reader.ReadInt();
		}
	}
}
