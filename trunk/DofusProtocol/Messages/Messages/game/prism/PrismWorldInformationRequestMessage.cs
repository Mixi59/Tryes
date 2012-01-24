// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'PrismWorldInformationRequestMessage.xml' the '24/01/2012 22:50:52'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class PrismWorldInformationRequestMessage : Message
	{
		public const uint Id = 5985;
		public override uint MessageId
		{
			get
			{
				return 5985;
			}
		}
		
		public bool join;
		
		public PrismWorldInformationRequestMessage()
		{
		}
		
		public PrismWorldInformationRequestMessage(bool join)
		{
			this.join = join;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteBoolean(join);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			join = reader.ReadBoolean();
		}
	}
}
