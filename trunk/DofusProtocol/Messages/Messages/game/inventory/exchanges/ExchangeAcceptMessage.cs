// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ExchangeAcceptMessage.xml' the '09/12/2011 21:48:34'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ExchangeAcceptMessage : Message
	{
		public const uint Id = 5508;
		public override uint MessageId
		{
			get
			{
				return 5508;
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
