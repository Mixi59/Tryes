// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'RawDataMessage.xml' the '09/12/2011 21:48:38'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class RawDataMessage : Message
	{
		public const uint Id = 6253;
		public override uint MessageId
		{
			get
			{
				return 6253;
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
