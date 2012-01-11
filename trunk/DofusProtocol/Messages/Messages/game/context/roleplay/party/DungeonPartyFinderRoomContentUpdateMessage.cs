// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'DungeonPartyFinderRoomContentUpdateMessage.xml' the '09/12/2011 21:48:31'
using System;
using Stump.Core.IO;
using System.Collections.Generic;
using System.Linq;

namespace Stump.DofusProtocol.Messages
{
	public class DungeonPartyFinderRoomContentUpdateMessage : Message
	{
		public const uint Id = 6250;
		public override uint MessageId
		{
			get
			{
				return 6250;
			}
		}
		
		public short dungeonId;
		public IEnumerable<Types.DungeonPartyFinderPlayer> addedPlayers;
		public IEnumerable<int> removedPlayersIds;
		
		public DungeonPartyFinderRoomContentUpdateMessage()
		{
		}
		
		public DungeonPartyFinderRoomContentUpdateMessage(short dungeonId, IEnumerable<Types.DungeonPartyFinderPlayer> addedPlayers, IEnumerable<int> removedPlayersIds)
		{
			this.dungeonId = dungeonId;
			this.addedPlayers = addedPlayers;
			this.removedPlayersIds = removedPlayersIds;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteShort(dungeonId);
			writer.WriteUShort((ushort)addedPlayers.Count());
			foreach (var entry in addedPlayers)
			{
				entry.Serialize(writer);
			}
			writer.WriteUShort((ushort)removedPlayersIds.Count());
			foreach (var entry in removedPlayersIds)
			{
				writer.WriteInt(entry);
			}
		}
		
		public override void Deserialize(IDataReader reader)
		{
			dungeonId = reader.ReadShort();
			if ( dungeonId < 0 )
			{
				throw new Exception("Forbidden value on dungeonId = " + dungeonId + ", it doesn't respect the following condition : dungeonId < 0");
			}
			int limit = reader.ReadUShort();
			addedPlayers = new Types.DungeonPartyFinderPlayer[limit];
			for (int i = 0; i < limit; i++)
			{
				(addedPlayers as Types.DungeonPartyFinderPlayer[])[i] = new Types.DungeonPartyFinderPlayer();
				(addedPlayers as Types.DungeonPartyFinderPlayer[])[i].Deserialize(reader);
			}
			limit = reader.ReadUShort();
			removedPlayersIds = new int[limit];
			for (int i = 0; i < limit; i++)
			{
				(removedPlayersIds as int[])[i] = reader.ReadInt();
			}
		}
	}
}
