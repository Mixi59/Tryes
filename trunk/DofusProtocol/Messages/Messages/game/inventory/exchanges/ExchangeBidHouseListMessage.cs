// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ExchangeBidHouseListMessage.xml' the '09/12/2011 21:48:34'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ExchangeBidHouseListMessage : Message
	{
		public const uint Id = 5807;
		public override uint MessageId
		{
			get
			{
				return 5807;
			}
		}
		
		public int id;
		
		public ExchangeBidHouseListMessage()
		{
		}
		
		public ExchangeBidHouseListMessage(int id)
		{
			this.id = id;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteInt(id);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			id = reader.ReadInt();
			if ( id < 0 )
			{
				throw new Exception("Forbidden value on id = " + id + ", it doesn't respect the following condition : id < 0");
			}
		}
	}
}
