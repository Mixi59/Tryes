// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'TeleportOnSameMapMessage.xml' the '09/12/2011 21:48:29'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class TeleportOnSameMapMessage : Message
	{
		public const uint Id = 6048;
		public override uint MessageId
		{
			get
			{
				return 6048;
			}
		}
		
		public int targetId;
		public short cellId;
		
		public TeleportOnSameMapMessage()
		{
		}
		
		public TeleportOnSameMapMessage(int targetId, short cellId)
		{
			this.targetId = targetId;
			this.cellId = cellId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteInt(targetId);
			writer.WriteShort(cellId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			targetId = reader.ReadInt();
			cellId = reader.ReadShort();
			if ( cellId < 0 || cellId > 559 )
			{
				throw new Exception("Forbidden value on cellId = " + cellId + ", it doesn't respect the following condition : cellId < 0 || cellId > 559");
			}
		}
	}
}
