// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'DownloadCurrentSpeedMessage.xml' the '09/12/2011 21:48:38'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class DownloadCurrentSpeedMessage : Message
	{
		public const uint Id = 1511;
		public override uint MessageId
		{
			get
			{
				return 1511;
			}
		}
		
		public sbyte downloadSpeed;
		
		public DownloadCurrentSpeedMessage()
		{
		}
		
		public DownloadCurrentSpeedMessage(sbyte downloadSpeed)
		{
			this.downloadSpeed = downloadSpeed;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteSByte(downloadSpeed);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			downloadSpeed = reader.ReadSByte();
			if ( downloadSpeed < 1 || downloadSpeed > 10 )
			{
				throw new Exception("Forbidden value on downloadSpeed = " + downloadSpeed + ", it doesn't respect the following condition : downloadSpeed < 1 || downloadSpeed > 10");
			}
		}
	}
}
