// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'JobAllowMultiCraftRequestMessage.xml' the '24/01/2012 22:50:43'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class JobAllowMultiCraftRequestMessage : Message
	{
		public const uint Id = 5748;
		public override uint MessageId
		{
			get
			{
				return 5748;
			}
		}
		
		public bool enabled;
		
		public JobAllowMultiCraftRequestMessage()
		{
		}
		
		public JobAllowMultiCraftRequestMessage(bool enabled)
		{
			this.enabled = enabled;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteBoolean(enabled);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			enabled = reader.ReadBoolean();
		}
	}
}
