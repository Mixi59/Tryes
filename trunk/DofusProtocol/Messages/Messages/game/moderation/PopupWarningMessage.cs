// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'PopupWarningMessage.xml' the '09/12/2011 21:48:37'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class PopupWarningMessage : Message
	{
		public const uint Id = 6134;
		public override uint MessageId
		{
			get
			{
				return 6134;
			}
		}
		
		public byte lockDuration;
		public string author;
		public string content;
		
		public PopupWarningMessage()
		{
		}
		
		public PopupWarningMessage(byte lockDuration, string author, string content)
		{
			this.lockDuration = lockDuration;
			this.author = author;
			this.content = content;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteByte(lockDuration);
			writer.WriteUTF(author);
			writer.WriteUTF(content);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			lockDuration = reader.ReadByte();
			if ( lockDuration < 0 || lockDuration > 255 )
			{
				throw new Exception("Forbidden value on lockDuration = " + lockDuration + ", it doesn't respect the following condition : lockDuration < 0 || lockDuration > 255");
			}
			author = reader.ReadUTF();
			content = reader.ReadUTF();
		}
	}
}
