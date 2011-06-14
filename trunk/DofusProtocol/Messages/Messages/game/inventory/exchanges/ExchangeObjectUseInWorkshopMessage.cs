// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ExchangeObjectUseInWorkshopMessage.xml' the '15/06/2011 01:39:03'
using System;
using Stump.BaseCore.Framework.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ExchangeObjectUseInWorkshopMessage : Message
	{
		public const uint Id = 6004;
		public override uint MessageId
		{
			get
			{
				return 6004;
			}
		}
		
		public int objectUID;
		public int quantity;
		
		public ExchangeObjectUseInWorkshopMessage()
		{
		}
		
		public ExchangeObjectUseInWorkshopMessage(int objectUID, int quantity)
		{
			this.objectUID = objectUID;
			this.quantity = quantity;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteInt(objectUID);
			writer.WriteInt(quantity);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			objectUID = reader.ReadInt();
			if ( objectUID < 0 )
			{
				throw new Exception("Forbidden value on objectUID = " + objectUID + ", it doesn't respect the following condition : objectUID < 0");
			}
			quantity = reader.ReadInt();
		}
	}
}
