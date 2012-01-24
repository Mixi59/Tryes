// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'LockableCodeResultMessage.xml' the '24/01/2012 22:50:44'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class LockableCodeResultMessage : Message
	{
		public const uint Id = 5672;
		public override uint MessageId
		{
			get
			{
				return 5672;
			}
		}
		
		public bool success;
		
		public LockableCodeResultMessage()
		{
		}
		
		public LockableCodeResultMessage(bool success)
		{
			this.success = success;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteBoolean(success);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			success = reader.ReadBoolean();
		}
	}
}
