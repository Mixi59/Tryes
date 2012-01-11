// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'DisplayNumericalValueMessage.xml' the '09/12/2011 21:48:28'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class DisplayNumericalValueMessage : Message
	{
		public const uint Id = 5808;
		public override uint MessageId
		{
			get
			{
				return 5808;
			}
		}
		
		public int entityId;
		public int value;
		public sbyte type;
		
		public DisplayNumericalValueMessage()
		{
		}
		
		public DisplayNumericalValueMessage(int entityId, int value, sbyte type)
		{
			this.entityId = entityId;
			this.value = value;
			this.type = type;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteInt(entityId);
			writer.WriteInt(value);
			writer.WriteSByte(type);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			entityId = reader.ReadInt();
			value = reader.ReadInt();
			type = reader.ReadSByte();
			if ( type < 0 )
			{
				throw new Exception("Forbidden value on type = " + type + ", it doesn't respect the following condition : type < 0");
			}
		}
	}
}
