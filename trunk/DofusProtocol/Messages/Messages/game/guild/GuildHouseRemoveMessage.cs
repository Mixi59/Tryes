// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GuildHouseRemoveMessage.xml' the '15/06/2011 01:38:59'
using System;
using Stump.BaseCore.Framework.IO;

namespace Stump.DofusProtocol.Messages
{
	public class GuildHouseRemoveMessage : Message
	{
		public const uint Id = 6180;
		public override uint MessageId
		{
			get
			{
				return 6180;
			}
		}
		
		public int houseId;
		
		public GuildHouseRemoveMessage()
		{
		}
		
		public GuildHouseRemoveMessage(int houseId)
		{
			this.houseId = houseId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteInt(houseId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			houseId = reader.ReadInt();
			if ( houseId < 0 )
			{
				throw new Exception("Forbidden value on houseId = " + houseId + ", it doesn't respect the following condition : houseId < 0");
			}
		}
	}
}
