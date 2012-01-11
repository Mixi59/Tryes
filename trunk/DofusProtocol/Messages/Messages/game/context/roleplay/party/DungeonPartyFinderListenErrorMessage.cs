// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'DungeonPartyFinderListenErrorMessage.xml' the '09/12/2011 21:48:30'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class DungeonPartyFinderListenErrorMessage : Message
	{
		public const uint Id = 6248;
		public override uint MessageId
		{
			get
			{
				return 6248;
			}
		}
		
		public short dungeonId;
		
		public DungeonPartyFinderListenErrorMessage()
		{
		}
		
		public DungeonPartyFinderListenErrorMessage(short dungeonId)
		{
			this.dungeonId = dungeonId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteShort(dungeonId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			dungeonId = reader.ReadShort();
			if ( dungeonId < 0 )
			{
				throw new Exception("Forbidden value on dungeonId = " + dungeonId + ", it doesn't respect the following condition : dungeonId < 0");
			}
		}
	}
}
