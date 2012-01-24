// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ExchangeClearPaymentForCraftMessage.xml' the '24/01/2012 22:50:48'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ExchangeClearPaymentForCraftMessage : Message
	{
		public const uint Id = 6145;
		public override uint MessageId
		{
			get
			{
				return 6145;
			}
		}
		
		public sbyte paymentType;
		
		public ExchangeClearPaymentForCraftMessage()
		{
		}
		
		public ExchangeClearPaymentForCraftMessage(sbyte paymentType)
		{
			this.paymentType = paymentType;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteSByte(paymentType);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			paymentType = reader.ReadSByte();
		}
	}
}
