// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'DungeonKeyRingMessage.xml' the '09/12/2011 21:48:28'
using System;
using Stump.Core.IO;
using System.Collections.Generic;
using System.Linq;

namespace Stump.DofusProtocol.Messages
{
	public class DungeonKeyRingMessage : Message
	{
		public const uint Id = 6299;
		public override uint MessageId
		{
			get
			{
				return 6299;
			}
		}
		
		public IEnumerable<short> availables;
		public IEnumerable<short> unavailables;
		
		public DungeonKeyRingMessage()
		{
		}
		
		public DungeonKeyRingMessage(IEnumerable<short> availables, IEnumerable<short> unavailables)
		{
			this.availables = availables;
			this.unavailables = unavailables;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteUShort((ushort)availables.Count());
			foreach (var entry in availables)
			{
				writer.WriteShort(entry);
			}
			writer.WriteUShort((ushort)unavailables.Count());
			foreach (var entry in unavailables)
			{
				writer.WriteShort(entry);
			}
		}
		
		public override void Deserialize(IDataReader reader)
		{
			int limit = reader.ReadUShort();
			availables = new short[limit];
			for (int i = 0; i < limit; i++)
			{
				(availables as short[])[i] = reader.ReadShort();
			}
			limit = reader.ReadUShort();
			unavailables = new short[limit];
			for (int i = 0; i < limit; i++)
			{
				(unavailables as short[])[i] = reader.ReadShort();
			}
		}
	}
}
