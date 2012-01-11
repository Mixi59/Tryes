// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'MonsterInGroupInformations.xml' the '09/12/2011 21:48:39'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
	public class MonsterInGroupInformations
	{
		public const uint Id = 144;
		public virtual short TypeId
		{
			get
			{
				return 144;
			}
		}
		
		public int creatureGenericId;
		public sbyte grade;
		public Types.EntityLook look;
		
		public MonsterInGroupInformations()
		{
		}
		
		public MonsterInGroupInformations(int creatureGenericId, sbyte grade, Types.EntityLook look)
		{
			this.creatureGenericId = creatureGenericId;
			this.grade = grade;
			this.look = look;
		}
		
		public virtual void Serialize(IDataWriter writer)
		{
			writer.WriteInt(creatureGenericId);
			writer.WriteSByte(grade);
			look.Serialize(writer);
		}
		
		public virtual void Deserialize(IDataReader reader)
		{
			creatureGenericId = reader.ReadInt();
			grade = reader.ReadSByte();
			if ( grade < 0 )
			{
				throw new Exception("Forbidden value on grade = " + grade + ", it doesn't respect the following condition : grade < 0");
			}
			look = new Types.EntityLook();
			look.Deserialize(reader);
		}
	}
}
