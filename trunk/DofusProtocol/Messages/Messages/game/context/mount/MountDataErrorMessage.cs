// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'MountDataErrorMessage.xml' the '15/06/2011 01:38:49'
using System;
using Stump.BaseCore.Framework.IO;

namespace Stump.DofusProtocol.Messages
{
	public class MountDataErrorMessage : Message
	{
		public const uint Id = 6172;
		public override uint MessageId
		{
			get
			{
				return 6172;
			}
		}
		
		public byte reason;
		
		public MountDataErrorMessage()
		{
		}
		
		public MountDataErrorMessage(byte reason)
		{
			this.reason = reason;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteByte(reason);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			reason = reader.ReadByte();
			if ( reason < 0 )
			{
				throw new Exception("Forbidden value on reason = " + reason + ", it doesn't respect the following condition : reason < 0");
			}
		}
	}
}
