// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'EmotePlayErrorMessage.xml' the '24/01/2012 22:50:43'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class EmotePlayErrorMessage : Message
	{
		public const uint Id = 5688;
		public override uint MessageId
		{
			get
			{
				return 5688;
			}
		}
		
		public sbyte emoteId;
		
		public EmotePlayErrorMessage()
		{
		}
		
		public EmotePlayErrorMessage(sbyte emoteId)
		{
			this.emoteId = emoteId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteSByte(emoteId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			emoteId = reader.ReadSByte();
		}
	}
}
