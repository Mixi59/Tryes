// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'DownloadGetCurrentSpeedRequestMessage.xml' the '24/01/2012 22:50:53'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class DownloadGetCurrentSpeedRequestMessage : Message
	{
		public const uint Id = 1510;
		public override uint MessageId
		{
			get
			{
				return 1510;
			}
		}
		
		
		public override void Serialize(IDataWriter writer)
		{
		}
		
		public override void Deserialize(IDataReader reader)
		{
		}
	}
}
