// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ExchangeReplayStopMessage.xml' the '24/01/2012 22:50:49'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ExchangeReplayStopMessage : Message
	{
		public const uint Id = 6001;
		public override uint MessageId
		{
			get
			{
				return 6001;
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
