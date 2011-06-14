// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GameActionMarkedCell.xml' the '14/06/2011 11:32:44'
using System;
using Stump.BaseCore.Framework.IO;

namespace Stump.DofusProtocol.Types
{
	public class GameActionMarkedCell
	{
		public const uint Id = 85;
		public short TypeId
		{
			get
			{
				return 85;
			}
		}
		
		public short cellId;
		public byte zoneSize;
		public int cellColor;
		public byte cellsType;
		
		public GameActionMarkedCell()
		{
		}
		
		public GameActionMarkedCell(short cellId, byte zoneSize, int cellColor, byte cellsType)
		{
			this.cellId = cellId;
			this.zoneSize = zoneSize;
			this.cellColor = cellColor;
			this.cellsType = cellsType;
		}
		
		public virtual void Serialize(IDataWriter writer)
		{
			writer.WriteShort(cellId);
			writer.WriteByte(zoneSize);
			writer.WriteInt(cellColor);
			writer.WriteByte(cellsType);
		}
		
		public virtual void Deserialize(IDataReader reader)
		{
			cellId = reader.ReadShort();
			if ( cellId < 0 || cellId > 559 )
			{
				throw new Exception("Forbidden value on cellId = " + cellId + ", it doesn't respect the following condition : cellId < 0 || cellId > 559");
			}
			zoneSize = reader.ReadByte();
			cellColor = reader.ReadInt();
			cellsType = reader.ReadByte();
		}
	}
}
