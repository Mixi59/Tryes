// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ExchangeShowVendorTaxMessage.xml' the '15/06/2011 01:39:04'
using System;
using Stump.BaseCore.Framework.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ExchangeShowVendorTaxMessage : Message
	{
		public const uint Id = 5783;
		public override uint MessageId
		{
			get
			{
				return 5783;
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
