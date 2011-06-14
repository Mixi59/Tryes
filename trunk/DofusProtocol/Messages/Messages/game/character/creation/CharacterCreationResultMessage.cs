// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'CharacterCreationResultMessage.xml' the '15/06/2011 01:38:44'
using System;
using Stump.BaseCore.Framework.IO;

namespace Stump.DofusProtocol.Messages
{
	public class CharacterCreationResultMessage : Message
	{
		public const uint Id = 161;
		public override uint MessageId
		{
			get
			{
				return 161;
			}
		}
		
		public byte result;
		
		public CharacterCreationResultMessage()
		{
		}
		
		public CharacterCreationResultMessage(byte result)
		{
			this.result = result;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteByte(result);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			result = reader.ReadByte();
			if ( result < 0 )
			{
				throw new Exception("Forbidden value on result = " + result + ", it doesn't respect the following condition : result < 0");
			}
		}
	}
}
