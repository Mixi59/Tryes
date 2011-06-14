// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'CharacterCreationRequestMessage.xml' the '15/06/2011 01:38:44'
using System;
using Stump.BaseCore.Framework.IO;

namespace Stump.DofusProtocol.Messages
{
	public class CharacterCreationRequestMessage : Message
	{
		public const uint Id = 160;
		public override uint MessageId
		{
			get
			{
				return 160;
			}
		}
		
		public string name;
		public byte breed;
		public bool sex;
		
		public CharacterCreationRequestMessage()
		{
		}
		
		public CharacterCreationRequestMessage(string name, byte breed, bool sex)
		{
			this.name = name;
			this.breed = breed;
			this.sex = sex;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteUTF(name);
			writer.WriteByte(breed);
			writer.WriteBoolean(sex);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			name = reader.ReadUTF();
			breed = reader.ReadByte();
			if ( breed < (byte)Enums.BreedEnum.Feca || breed > (byte)Enums.BreedEnum.Zobal )
			{
				throw new Exception("Forbidden value on breed = " + breed + ", it doesn't respect the following condition : breed < (byte)Enums.BreedEnum.Feca || breed > (byte)Enums.BreedEnum.Zobal");
			}
			sex = reader.ReadBoolean();
		}
	}
}
