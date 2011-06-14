// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ObjectDeleteMessage.xml' the '15/06/2011 01:39:06'
using System;
using Stump.BaseCore.Framework.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ObjectDeleteMessage : Message
	{
		public const uint Id = 3022;
		public override uint MessageId
		{
			get
			{
				return 3022;
			}
		}
		
		public int objectUID;
		public int quantity;
		
		public ObjectDeleteMessage()
		{
		}
		
		public ObjectDeleteMessage(int objectUID, int quantity)
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
			if ( quantity < 0 )
			{
				throw new Exception("Forbidden value on quantity = " + quantity + ", it doesn't respect the following condition : quantity < 0");
			}
		}
	}
}
