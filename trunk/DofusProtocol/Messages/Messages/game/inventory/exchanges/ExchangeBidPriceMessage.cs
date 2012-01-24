// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ExchangeBidPriceMessage.xml' the '24/01/2012 22:50:48'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ExchangeBidPriceMessage : Message
	{
		public const uint Id = 5755;
		public override uint MessageId
		{
			get
			{
				return 5755;
			}
		}
		
		public int genericId;
		public int averagePrice;
		
		public ExchangeBidPriceMessage()
		{
		}
		
		public ExchangeBidPriceMessage(int genericId, int averagePrice)
		{
			this.genericId = genericId;
			this.averagePrice = averagePrice;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteInt(genericId);
			writer.WriteInt(averagePrice);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			genericId = reader.ReadInt();
			if ( genericId < 0 )
			{
				throw new Exception("Forbidden value on genericId = " + genericId + ", it doesn't respect the following condition : genericId < 0");
			}
			averagePrice = reader.ReadInt();
			if ( averagePrice < 0 )
			{
				throw new Exception("Forbidden value on averagePrice = " + averagePrice + ", it doesn't respect the following condition : averagePrice < 0");
			}
		}
	}
}
