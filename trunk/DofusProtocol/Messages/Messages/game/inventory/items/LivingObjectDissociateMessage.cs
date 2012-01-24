// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'LivingObjectDissociateMessage.xml' the '24/01/2012 22:50:50'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class LivingObjectDissociateMessage : Message
	{
		public const uint Id = 5723;
		public override uint MessageId
		{
			get
			{
				return 5723;
			}
		}
		
		public int livingUID;
		public byte livingPosition;
		
		public LivingObjectDissociateMessage()
		{
		}
		
		public LivingObjectDissociateMessage(int livingUID, byte livingPosition)
		{
			this.livingUID = livingUID;
			this.livingPosition = livingPosition;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteInt(livingUID);
			writer.WriteByte(livingPosition);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			livingUID = reader.ReadInt();
			if ( livingUID < 0 )
			{
				throw new Exception("Forbidden value on livingUID = " + livingUID + ", it doesn't respect the following condition : livingUID < 0");
			}
			livingPosition = reader.ReadByte();
			if ( livingPosition < 0 || livingPosition > 255 )
			{
				throw new Exception("Forbidden value on livingPosition = " + livingPosition + ", it doesn't respect the following condition : livingPosition < 0 || livingPosition > 255");
			}
		}
	}
}
