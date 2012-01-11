// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'CurrentMapMessage.xml' the '09/12/2011 21:48:29'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class CurrentMapMessage : Message
	{
		public const uint Id = 220;
		public override uint MessageId
		{
			get
			{
				return 220;
			}
		}
		
		public int mapId;
		
		public CurrentMapMessage()
		{
		}
		
		public CurrentMapMessage(int mapId)
		{
			this.mapId = mapId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteInt(mapId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			mapId = reader.ReadInt();
			if ( mapId < 0 )
			{
				throw new Exception("Forbidden value on mapId = " + mapId + ", it doesn't respect the following condition : mapId < 0");
			}
		}
	}
}
