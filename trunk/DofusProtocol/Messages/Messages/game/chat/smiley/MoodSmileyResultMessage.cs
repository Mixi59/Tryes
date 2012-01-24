// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'MoodSmileyResultMessage.xml' the '24/01/2012 22:50:40'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class MoodSmileyResultMessage : Message
	{
		public const uint Id = 6196;
		public override uint MessageId
		{
			get
			{
				return 6196;
			}
		}
		
		public sbyte resultCode;
		public sbyte smileyId;
		
		public MoodSmileyResultMessage()
		{
		}
		
		public MoodSmileyResultMessage(sbyte resultCode, sbyte smileyId)
		{
			this.resultCode = resultCode;
			this.smileyId = smileyId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteSByte(resultCode);
			writer.WriteSByte(smileyId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			resultCode = reader.ReadSByte();
			if ( resultCode < 0 )
			{
				throw new Exception("Forbidden value on resultCode = " + resultCode + ", it doesn't respect the following condition : resultCode < 0");
			}
			smileyId = reader.ReadSByte();
		}
	}
}
