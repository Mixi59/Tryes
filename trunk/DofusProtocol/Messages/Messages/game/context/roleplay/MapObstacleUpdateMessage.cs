// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'MapObstacleUpdateMessage.xml' the '15/06/2011 01:38:50'
using System;
using Stump.BaseCore.Framework.IO;

namespace Stump.DofusProtocol.Messages
{
	public class MapObstacleUpdateMessage : Message
	{
		public const uint Id = 6051;
		public override uint MessageId
		{
			get
			{
				return 6051;
			}
		}
		
		public Types.MapObstacle[] obstacles;
		
		public MapObstacleUpdateMessage()
		{
		}
		
		public MapObstacleUpdateMessage(Types.MapObstacle[] obstacles)
		{
			this.obstacles = obstacles;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteUShort((ushort)obstacles.Length);
			for (int i = 0; i < obstacles.Length; i++)
			{
				obstacles[i].Serialize(writer);
			}
		}
		
		public override void Deserialize(IDataReader reader)
		{
			int limit = reader.ReadUShort();
			obstacles = new Types.MapObstacle[limit];
			for (int i = 0; i < limit; i++)
			{
				obstacles[i] = new Types.MapObstacle();
				obstacles[i].Deserialize(reader);
			}
		}
	}
}
