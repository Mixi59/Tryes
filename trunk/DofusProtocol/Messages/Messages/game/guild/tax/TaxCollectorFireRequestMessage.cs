// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'TaxCollectorFireRequestMessage.xml' the '09/12/2011 21:48:33'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class TaxCollectorFireRequestMessage : Message
	{
		public const uint Id = 5682;
		public override uint MessageId
		{
			get
			{
				return 5682;
			}
		}
		
		public int collectorId;
		
		public TaxCollectorFireRequestMessage()
		{
		}
		
		public TaxCollectorFireRequestMessage(int collectorId)
		{
			this.collectorId = collectorId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteInt(collectorId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			collectorId = reader.ReadInt();
		}
	}
}
