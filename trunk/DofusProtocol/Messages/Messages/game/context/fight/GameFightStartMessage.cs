// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GameFightStartMessage.xml' the '09/12/2011 21:48:28'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class GameFightStartMessage : Message
	{
		public const uint Id = 712;
		public override uint MessageId
		{
			get
			{
				return 712;
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
