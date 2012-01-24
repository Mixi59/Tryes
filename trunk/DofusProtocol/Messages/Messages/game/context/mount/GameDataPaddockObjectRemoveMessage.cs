// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GameDataPaddockObjectRemoveMessage.xml' the '24/01/2012 22:50:41'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class GameDataPaddockObjectRemoveMessage : Message
	{
		public const uint Id = 5993;
		public override uint MessageId
		{
			get
			{
				return 5993;
			}
		}
		
		public short cellId;
		
		public GameDataPaddockObjectRemoveMessage()
		{
		}
		
		public GameDataPaddockObjectRemoveMessage(short cellId)
		{
			this.cellId = cellId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteShort(cellId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			cellId = reader.ReadShort();
			if ( cellId < 0 || cellId > 559 )
			{
				throw new Exception("Forbidden value on cellId = " + cellId + ", it doesn't respect the following condition : cellId < 0 || cellId > 559");
			}
		}
	}
}
