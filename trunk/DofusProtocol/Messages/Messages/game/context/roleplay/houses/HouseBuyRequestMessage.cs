// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'HouseBuyRequestMessage.xml' the '15/06/2011 01:38:51'
using System;
using Stump.BaseCore.Framework.IO;

namespace Stump.DofusProtocol.Messages
{
	public class HouseBuyRequestMessage : Message
	{
		public const uint Id = 5738;
		public override uint MessageId
		{
			get
			{
				return 5738;
			}
		}
		
		public int proposedPrice;
		
		public HouseBuyRequestMessage()
		{
		}
		
		public HouseBuyRequestMessage(int proposedPrice)
		{
			this.proposedPrice = proposedPrice;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteInt(proposedPrice);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			proposedPrice = reader.ReadInt();
			if ( proposedPrice < 0 )
			{
				throw new Exception("Forbidden value on proposedPrice = " + proposedPrice + ", it doesn't respect the following condition : proposedPrice < 0");
			}
		}
	}
}
