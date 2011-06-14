// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'MonsterInGroupInformations.xml' the '14/06/2011 11:32:47'
using System;
using Stump.BaseCore.Framework.IO;

namespace Stump.DofusProtocol.Types
{
	public class MonsterInGroupInformations
	{
		public const uint Id = 144;
		public short TypeId
		{
			get
			{
				return 144;
			}
		}
		
		public int creatureGenericId;
		public short level;
		public Types.EntityLook look;
		
		public MonsterInGroupInformations()
		{
		}
		
		public MonsterInGroupInformations(int creatureGenericId, short level, Types.EntityLook look)
		{
			this.creatureGenericId = creatureGenericId;
			this.level = level;
			this.look = look;
		}
		
		public virtual void Serialize(IDataWriter writer)
		{
			writer.WriteInt(creatureGenericId);
			writer.WriteShort(level);
			look.Serialize(writer);
		}
		
		public virtual void Deserialize(IDataReader reader)
		{
			creatureGenericId = reader.ReadInt();
			level = reader.ReadShort();
			if ( level < 0 )
			{
				throw new Exception("Forbidden value on level = " + level + ", it doesn't respect the following condition : level < 0");
			}
			look = new Types.EntityLook();
			look.Deserialize(reader);
		}
	}
}
