// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GameContextMoveMultipleElementsMessage.xml' the '24/01/2012 22:50:40'
using System;
using Stump.Core.IO;
using System.Collections.Generic;
using System.Linq;

namespace Stump.DofusProtocol.Messages
{
	public class GameContextMoveMultipleElementsMessage : Message
	{
		public const uint Id = 254;
		public override uint MessageId
		{
			get
			{
				return 254;
			}
		}
		
		public IEnumerable<Types.EntityMovementInformations> movements;
		
		public GameContextMoveMultipleElementsMessage()
		{
		}
		
		public GameContextMoveMultipleElementsMessage(IEnumerable<Types.EntityMovementInformations> movements)
		{
			this.movements = movements;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteUShort((ushort)movements.Count());
			foreach (var entry in movements)
			{
				entry.Serialize(writer);
			}
		}
		
		public override void Deserialize(IDataReader reader)
		{
			int limit = reader.ReadUShort();
			movements = new Types.EntityMovementInformations[limit];
			for (int i = 0; i < limit; i++)
			{
				(movements as Types.EntityMovementInformations[])[i] = new Types.EntityMovementInformations();
				(movements as Types.EntityMovementInformations[])[i].Deserialize(reader);
			}
		}
	}
}
