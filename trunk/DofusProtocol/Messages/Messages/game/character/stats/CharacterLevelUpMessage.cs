// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'CharacterLevelUpMessage.xml' the '09/12/2011 21:48:26'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class CharacterLevelUpMessage : Message
	{
		public const uint Id = 5670;
		public override uint MessageId
		{
			get
			{
				return 5670;
			}
		}
		
		public byte newLevel;
		
		public CharacterLevelUpMessage()
		{
		}
		
		public CharacterLevelUpMessage(byte newLevel)
		{
			this.newLevel = newLevel;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteByte(newLevel);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			newLevel = reader.ReadByte();
			if ( newLevel < 2 || newLevel > 200 )
			{
				throw new Exception("Forbidden value on newLevel = " + newLevel + ", it doesn't respect the following condition : newLevel < 2 || newLevel > 200");
			}
		}
	}
}
