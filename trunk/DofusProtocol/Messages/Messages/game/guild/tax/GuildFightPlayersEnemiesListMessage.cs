// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GuildFightPlayersEnemiesListMessage.xml' the '24/01/2012 22:50:47'
using System;
using Stump.Core.IO;
using System.Collections.Generic;
using System.Linq;

namespace Stump.DofusProtocol.Messages
{
	public class GuildFightPlayersEnemiesListMessage : Message
	{
		public const uint Id = 5928;
		public override uint MessageId
		{
			get
			{
				return 5928;
			}
		}
		
		public double fightId;
		public IEnumerable<Types.CharacterMinimalPlusLookInformations> playerInfo;
		
		public GuildFightPlayersEnemiesListMessage()
		{
		}
		
		public GuildFightPlayersEnemiesListMessage(double fightId, IEnumerable<Types.CharacterMinimalPlusLookInformations> playerInfo)
		{
			this.fightId = fightId;
			this.playerInfo = playerInfo;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteDouble(fightId);
			writer.WriteUShort((ushort)playerInfo.Count());
			foreach (var entry in playerInfo)
			{
				entry.Serialize(writer);
			}
		}
		
		public override void Deserialize(IDataReader reader)
		{
			fightId = reader.ReadDouble();
			if ( fightId < 0 )
			{
				throw new Exception("Forbidden value on fightId = " + fightId + ", it doesn't respect the following condition : fightId < 0");
			}
			int limit = reader.ReadUShort();
			playerInfo = new Types.CharacterMinimalPlusLookInformations[limit];
			for (int i = 0; i < limit; i++)
			{
				(playerInfo as Types.CharacterMinimalPlusLookInformations[])[i] = new Types.CharacterMinimalPlusLookInformations();
				(playerInfo as Types.CharacterMinimalPlusLookInformations[])[i].Deserialize(reader);
			}
		}
	}
}
