// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GuildEmblem.xml' the '24/01/2012 22:50:55'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
	public class GuildEmblem
	{
		public const uint Id = 87;
		public virtual short TypeId
		{
			get
			{
				return 87;
			}
		}
		
		public short symbolShape;
		public int symbolColor;
		public short backgroundShape;
		public int backgroundColor;
		
		public GuildEmblem()
		{
		}
		
		public GuildEmblem(short symbolShape, int symbolColor, short backgroundShape, int backgroundColor)
		{
			this.symbolShape = symbolShape;
			this.symbolColor = symbolColor;
			this.backgroundShape = backgroundShape;
			this.backgroundColor = backgroundColor;
		}
		
		public virtual void Serialize(IDataWriter writer)
		{
			writer.WriteShort(symbolShape);
			writer.WriteInt(symbolColor);
			writer.WriteShort(backgroundShape);
			writer.WriteInt(backgroundColor);
		}
		
		public virtual void Deserialize(IDataReader reader)
		{
			symbolShape = reader.ReadShort();
			symbolColor = reader.ReadInt();
			backgroundShape = reader.ReadShort();
			backgroundColor = reader.ReadInt();
		}
	}
}
