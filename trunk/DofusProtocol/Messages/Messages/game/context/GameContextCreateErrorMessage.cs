// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GameContextCreateErrorMessage.xml' the '15/06/2011 01:38:46'
using System;
using Stump.BaseCore.Framework.IO;

namespace Stump.DofusProtocol.Messages
{
	public class GameContextCreateErrorMessage : Message
	{
		public const uint Id = 6024;
		public override uint MessageId
		{
			get
			{
				return 6024;
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
