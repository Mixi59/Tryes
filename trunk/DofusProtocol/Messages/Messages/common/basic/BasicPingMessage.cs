// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'BasicPingMessage.xml' the '15/06/2011 01:38:38'
using System;
using Stump.BaseCore.Framework.IO;

namespace Stump.DofusProtocol.Messages
{
	public class BasicPingMessage : Message
	{
		public const uint Id = 182;
		public override uint MessageId
		{
			get
			{
				return 182;
			}
		}
		
		public bool quiet;
		
		public BasicPingMessage()
		{
		}
		
		public BasicPingMessage(bool quiet)
		{
			this.quiet = quiet;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteBoolean(quiet);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			quiet = reader.ReadBoolean();
		}
	}
}
