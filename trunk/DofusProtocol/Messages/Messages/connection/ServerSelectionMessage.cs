// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ServerSelectionMessage.xml' the '15/06/2011 01:38:39'
using System;
using Stump.BaseCore.Framework.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ServerSelectionMessage : Message
	{
		public const uint Id = 40;
		public override uint MessageId
		{
			get
			{
				return 40;
			}
		}
		
		public short serverId;
		
		public ServerSelectionMessage()
		{
		}
		
		public ServerSelectionMessage(short serverId)
		{
			this.serverId = serverId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteShort(serverId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			serverId = reader.ReadShort();
		}
	}
}
