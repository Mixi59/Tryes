// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'CharacterBaseInformations.xml' the '09/12/2011 21:48:38'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
	public class CharacterBaseInformations : CharacterMinimalPlusLookInformations
	{
		public const uint Id = 45;
		public override short TypeId
		{
			get
			{
				return 45;
			}
		}
		
		public sbyte breed;
		public bool sex;
		
		public CharacterBaseInformations()
		{
		}
		
		public CharacterBaseInformations(int id, byte level, string name, Types.EntityLook entityLook, sbyte breed, bool sex)
			 : base(id, level, name, entityLook)
		{
			this.breed = breed;
			this.sex = sex;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteSByte(breed);
			writer.WriteBoolean(sex);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			breed = reader.ReadSByte();
			sex = reader.ReadBoolean();
		}
	}
}
