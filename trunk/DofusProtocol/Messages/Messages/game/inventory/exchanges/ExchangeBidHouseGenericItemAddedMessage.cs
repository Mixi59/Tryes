// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ExchangeBidHouseGenericItemAddedMessage.xml' the '15/06/2011 01:39:01'
using System;
using Stump.BaseCore.Framework.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ExchangeBidHouseGenericItemAddedMessage : Message
	{
		public const uint Id = 5947;
		public override uint MessageId
		{
			get
			{
				return 5947;
			}
		}
		
		public int objGenericId;
		
		public ExchangeBidHouseGenericItemAddedMessage()
		{
		}
		
		public ExchangeBidHouseGenericItemAddedMessage(int objGenericId)
		{
			this.objGenericId = objGenericId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteInt(objGenericId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			objGenericId = reader.ReadInt();
		}
	}
}
