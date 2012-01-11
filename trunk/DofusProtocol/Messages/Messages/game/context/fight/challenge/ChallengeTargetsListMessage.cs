// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ChallengeTargetsListMessage.xml' the '09/12/2011 21:48:28'
using System;
using Stump.Core.IO;
using System.Collections.Generic;
using System.Linq;

namespace Stump.DofusProtocol.Messages
{
	public class ChallengeTargetsListMessage : Message
	{
		public const uint Id = 5613;
		public override uint MessageId
		{
			get
			{
				return 5613;
			}
		}
		
		public IEnumerable<int> targetIds;
		public IEnumerable<short> targetCells;
		
		public ChallengeTargetsListMessage()
		{
		}
		
		public ChallengeTargetsListMessage(IEnumerable<int> targetIds, IEnumerable<short> targetCells)
		{
			this.targetIds = targetIds;
			this.targetCells = targetCells;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteUShort((ushort)targetIds.Count());
			foreach (var entry in targetIds)
			{
				writer.WriteInt(entry);
			}
			writer.WriteUShort((ushort)targetCells.Count());
			foreach (var entry in targetCells)
			{
				writer.WriteShort(entry);
			}
		}
		
		public override void Deserialize(IDataReader reader)
		{
			int limit = reader.ReadUShort();
			targetIds = new int[limit];
			for (int i = 0; i < limit; i++)
			{
				(targetIds as int[])[i] = reader.ReadInt();
			}
			limit = reader.ReadUShort();
			targetCells = new short[limit];
			for (int i = 0; i < limit; i++)
			{
				(targetCells as short[])[i] = reader.ReadShort();
			}
		}
	}
}
