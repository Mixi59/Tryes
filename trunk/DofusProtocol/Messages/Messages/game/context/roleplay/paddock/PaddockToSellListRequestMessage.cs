// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'PaddockToSellListRequestMessage.xml' the '24/01/2012 22:50:44'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class PaddockToSellListRequestMessage : Message
	{
		public const uint Id = 6141;
		public override uint MessageId
		{
			get
			{
				return 6141;
			}
		}
		
		public short pageIndex;
		
		public PaddockToSellListRequestMessage()
		{
		}
		
		public PaddockToSellListRequestMessage(short pageIndex)
		{
			this.pageIndex = pageIndex;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteShort(pageIndex);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			pageIndex = reader.ReadShort();
			if ( pageIndex < 0 )
			{
				throw new Exception("Forbidden value on pageIndex = " + pageIndex + ", it doesn't respect the following condition : pageIndex < 0");
			}
		}
	}
}
