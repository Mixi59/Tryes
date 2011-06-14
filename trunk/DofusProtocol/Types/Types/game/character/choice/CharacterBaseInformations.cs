// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'CharacterBaseInformations.xml' the '14/06/2011 11:32:45'
using System;
using Stump.BaseCore.Framework.IO;

namespace Stump.DofusProtocol.Types
{
	public class CharacterBaseInformations : CharacterMinimalPlusLookInformations
	{
		public const uint Id = 45;
		public short TypeId
		{
			get
			{
				return 45;
			}
		}
		
		public byte breed;
		public bool sex;
		
		public CharacterBaseInformations()
		{
		}
		
		public CharacterBaseInformations(int id, byte level, string name, Types.EntityLook entityLook, byte breed, bool sex)
			 : base(id, level, name, entityLook)
		{
			this.breed = breed;
			this.sex = sex;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteByte(breed);
			writer.WriteBoolean(sex);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			breed = reader.ReadByte();
			sex = reader.ReadBoolean();
		}
	}
}
