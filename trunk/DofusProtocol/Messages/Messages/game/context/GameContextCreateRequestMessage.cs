// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GameContextCreateRequestMessage.xml' the '15/06/2011 01:38:46'
using System;
using Stump.BaseCore.Framework.IO;

namespace Stump.DofusProtocol.Messages
{
	public class GameContextCreateRequestMessage : Message
	{
		public const uint Id = 250;
		public override uint MessageId
		{
			get
			{
				return 250;
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
