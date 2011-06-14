// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ShowCellRequestMessage.xml' the '15/06/2011 01:38:47'
using System;
using Stump.BaseCore.Framework.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ShowCellRequestMessage : Message
	{
		public const uint Id = 5611;
		public override uint MessageId
		{
			get
			{
				return 5611;
			}
		}
		
		public short cellId;
		
		public ShowCellRequestMessage()
		{
		}
		
		public ShowCellRequestMessage(short cellId)
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
