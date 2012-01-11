// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'BasicWhoIsRequestMessage.xml' the '09/12/2011 21:48:26'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class BasicWhoIsRequestMessage : Message
	{
		public const uint Id = 181;
		public override uint MessageId
		{
			get
			{
				return 181;
			}
		}
		
		public string search;
		
		public BasicWhoIsRequestMessage()
		{
		}
		
		public BasicWhoIsRequestMessage(string search)
		{
			this.search = search;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteUTF(search);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			search = reader.ReadUTF();
		}
	}
}
