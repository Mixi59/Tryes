// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'FightLoot.xml' the '24/01/2012 22:50:54'
using System;
using Stump.Core.IO;
using System.Collections.Generic;
using System.Linq;

namespace Stump.DofusProtocol.Types
{
	public class FightLoot
	{
		public const uint Id = 41;
		public virtual short TypeId
		{
			get
			{
				return 41;
			}
		}
		
		public IEnumerable<short> objects;
		public int kamas;
		
		public FightLoot()
		{
		}
		
		public FightLoot(IEnumerable<short> objects, int kamas)
		{
			this.objects = objects;
			this.kamas = kamas;
		}
		
		public virtual void Serialize(IDataWriter writer)
		{
			writer.WriteUShort((ushort)objects.Count());
			foreach (var entry in objects)
			{
				writer.WriteShort(entry);
			}
			writer.WriteInt(kamas);
		}
		
		public virtual void Deserialize(IDataReader reader)
		{
			int limit = reader.ReadUShort();
			objects = new short[limit];
			for (int i = 0; i < limit; i++)
			{
				(objects as short[])[i] = reader.ReadShort();
			}
			kamas = reader.ReadInt();
			if ( kamas < 0 )
			{
				throw new Exception("Forbidden value on kamas = " + kamas + ", it doesn't respect the following condition : kamas < 0");
			}
		}
	}
}
