// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ExchangeRequestOnMountStockMessage.xml' the '09/12/2011 21:48:35'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ExchangeRequestOnMountStockMessage : Message
	{
		public const uint Id = 5986;
		public override uint MessageId
		{
			get
			{
				return 5986;
			}
		}
		
		
		public override void Serialize(IDataWriter writer)
		{
		}
		
		public override void Deserialize(IDataReader reader)
		{
		}
	}
}
