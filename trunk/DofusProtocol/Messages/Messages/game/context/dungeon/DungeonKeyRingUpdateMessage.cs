// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'DungeonKeyRingUpdateMessage.xml' the '09/12/2011 21:48:28'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class DungeonKeyRingUpdateMessage : Message
	{
		public const uint Id = 6296;
		public override uint MessageId
		{
			get
			{
				return 6296;
			}
		}
		
		public short dungeonId;
		public bool available;
		
		public DungeonKeyRingUpdateMessage()
		{
		}
		
		public DungeonKeyRingUpdateMessage(short dungeonId, bool available)
		{
			this.dungeonId = dungeonId;
			this.available = available;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteShort(dungeonId);
			writer.WriteBoolean(available);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			dungeonId = reader.ReadShort();
			if ( dungeonId < 0 )
			{
				throw new Exception("Forbidden value on dungeonId = " + dungeonId + ", it doesn't respect the following condition : dungeonId < 0");
			}
			available = reader.ReadBoolean();
		}
	}
}