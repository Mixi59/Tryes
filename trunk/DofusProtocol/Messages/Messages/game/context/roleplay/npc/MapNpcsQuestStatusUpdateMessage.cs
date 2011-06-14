// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'MapNpcsQuestStatusUpdateMessage.xml' the '15/06/2011 01:38:53'
using System;
using Stump.BaseCore.Framework.IO;

namespace Stump.DofusProtocol.Messages
{
	public class MapNpcsQuestStatusUpdateMessage : Message
	{
		public const uint Id = 5642;
		public override uint MessageId
		{
			get
			{
				return 5642;
			}
		}
		
		public int mapId;
		public int[] npcsIdsCanGiveQuest;
		public int[] npcsIdsCannotGiveQuest;
		
		public MapNpcsQuestStatusUpdateMessage()
		{
		}
		
		public MapNpcsQuestStatusUpdateMessage(int mapId, int[] npcsIdsCanGiveQuest, int[] npcsIdsCannotGiveQuest)
		{
			this.mapId = mapId;
			this.npcsIdsCanGiveQuest = npcsIdsCanGiveQuest;
			this.npcsIdsCannotGiveQuest = npcsIdsCannotGiveQuest;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteInt(mapId);
			writer.WriteUShort((ushort)npcsIdsCanGiveQuest.Length);
			for (int i = 0; i < npcsIdsCanGiveQuest.Length; i++)
			{
				writer.WriteInt(npcsIdsCanGiveQuest[i]);
			}
			writer.WriteUShort((ushort)npcsIdsCannotGiveQuest.Length);
			for (int i = 0; i < npcsIdsCannotGiveQuest.Length; i++)
			{
				writer.WriteInt(npcsIdsCannotGiveQuest[i]);
			}
		}
		
		public override void Deserialize(IDataReader reader)
		{
			mapId = reader.ReadInt();
			int limit = reader.ReadUShort();
			npcsIdsCanGiveQuest = new int[limit];
			for (int i = 0; i < limit; i++)
			{
				npcsIdsCanGiveQuest[i] = reader.ReadInt();
			}
			limit = reader.ReadUShort();
			npcsIdsCannotGiveQuest = new int[limit];
			for (int i = 0; i < limit; i++)
			{
				npcsIdsCannotGiveQuest[i] = reader.ReadInt();
			}
		}
	}
}
