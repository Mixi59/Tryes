// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'MapObstacle.xml' the '14/06/2011 11:32:49'
using System;
using Stump.BaseCore.Framework.IO;

namespace Stump.DofusProtocol.Types
{
	public class MapObstacle
	{
		public const uint Id = 200;
		public short TypeId
		{
			get
			{
				return 200;
			}
		}
		
		public short obstacleCellId;
		public byte state;
		
		public MapObstacle()
		{
		}
		
		public MapObstacle(short obstacleCellId, byte state)
		{
			this.obstacleCellId = obstacleCellId;
			this.state = state;
		}
		
		public virtual void Serialize(IDataWriter writer)
		{
			writer.WriteShort(obstacleCellId);
			writer.WriteByte(state);
		}
		
		public virtual void Deserialize(IDataReader reader)
		{
			obstacleCellId = reader.ReadShort();
			if ( obstacleCellId < 0 || obstacleCellId > 559 )
			{
				throw new Exception("Forbidden value on obstacleCellId = " + obstacleCellId + ", it doesn't respect the following condition : obstacleCellId < 0 || obstacleCellId > 559");
			}
			state = reader.ReadByte();
			if ( state < 0 )
			{
				throw new Exception("Forbidden value on state = " + state + ", it doesn't respect the following condition : state < 0");
			}
		}
	}
}
