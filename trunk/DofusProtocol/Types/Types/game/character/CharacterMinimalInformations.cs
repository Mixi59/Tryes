// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'CharacterMinimalInformations.xml' the '09/12/2011 21:48:38'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
	public class CharacterMinimalInformations
	{
		public const uint Id = 110;
		public virtual short TypeId
		{
			get
			{
				return 110;
			}
		}
		
		public int id;
		public byte level;
		public string name;
		
		public CharacterMinimalInformations()
		{
		}
		
		public CharacterMinimalInformations(int id, byte level, string name)
		{
			this.id = id;
			this.level = level;
			this.name = name;
		}
		
		public virtual void Serialize(IDataWriter writer)
		{
			writer.WriteInt(id);
			writer.WriteByte(level);
			writer.WriteUTF(name);
		}
		
		public virtual void Deserialize(IDataReader reader)
		{
			id = reader.ReadInt();
			if ( id < 0 )
			{
				throw new Exception("Forbidden value on id = " + id + ", it doesn't respect the following condition : id < 0");
			}
			level = reader.ReadByte();
			if ( level < 1 || level > 200 )
			{
				throw new Exception("Forbidden value on level = " + level + ", it doesn't respect the following condition : level < 1 || level > 200");
			}
			name = reader.ReadUTF();
		}
	}
}
