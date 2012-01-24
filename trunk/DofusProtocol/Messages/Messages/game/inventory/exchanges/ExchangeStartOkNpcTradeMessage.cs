// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ExchangeStartOkNpcTradeMessage.xml' the '24/01/2012 22:50:50'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ExchangeStartOkNpcTradeMessage : Message
	{
		public const uint Id = 5785;
		public override uint MessageId
		{
			get
			{
				return 5785;
			}
		}
		
		public int npcId;
		
		public ExchangeStartOkNpcTradeMessage()
		{
		}
		
		public ExchangeStartOkNpcTradeMessage(int npcId)
		{
			this.npcId = npcId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteInt(npcId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			npcId = reader.ReadInt();
		}
	}
}
