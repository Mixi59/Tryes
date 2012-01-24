// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'TaxCollectorErrorMessage.xml' the '24/01/2012 22:50:47'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class TaxCollectorErrorMessage : Message
	{
		public const uint Id = 5634;
		public override uint MessageId
		{
			get
			{
				return 5634;
			}
		}
		
		public sbyte reason;
		
		public TaxCollectorErrorMessage()
		{
		}
		
		public TaxCollectorErrorMessage(sbyte reason)
		{
			this.reason = reason;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteSByte(reason);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			reason = reader.ReadSByte();
		}
	}
}
