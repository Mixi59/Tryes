// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ObjectEffects.xml' the '09/12/2011 21:48:39'
using System;
using Stump.Core.IO;
using System.Collections.Generic;
using System.Linq;

namespace Stump.DofusProtocol.Types
{
	public class ObjectEffects
	{
		public const uint Id = 358;
		public virtual short TypeId
		{
			get
			{
				return 358;
			}
		}
		
		public short powerRate;
		public bool overMax;
		public IEnumerable<Types.ObjectEffect> effects;
		
		public ObjectEffects()
		{
		}
		
		public ObjectEffects(short powerRate, bool overMax, IEnumerable<Types.ObjectEffect> effects)
		{
			this.powerRate = powerRate;
			this.overMax = overMax;
			this.effects = effects;
		}
		
		public virtual void Serialize(IDataWriter writer)
		{
			writer.WriteShort(powerRate);
			writer.WriteBoolean(overMax);
			writer.WriteUShort((ushort)effects.Count());
			foreach (var entry in effects)
			{
				writer.WriteShort(entry.TypeId);
				entry.Serialize(writer);
			}
		}
		
		public virtual void Deserialize(IDataReader reader)
		{
			powerRate = reader.ReadShort();
			overMax = reader.ReadBoolean();
			int limit = reader.ReadUShort();
			effects = new Types.ObjectEffect[limit];
			for (int i = 0; i < limit; i++)
			{
				(effects as Types.ObjectEffect[])[i] = ProtocolTypeManager.GetInstance<Types.ObjectEffect>(reader.ReadShort());
				(effects as Types.ObjectEffect[])[i].Deserialize(reader);
			}
		}
	}
}
