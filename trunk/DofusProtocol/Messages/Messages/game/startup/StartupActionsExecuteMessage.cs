// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'StartupActionsExecuteMessage.xml' the '15/06/2011 01:39:09'
using System;
using Stump.BaseCore.Framework.IO;

namespace Stump.DofusProtocol.Messages
{
	public class StartupActionsExecuteMessage : Message
	{
		public const uint Id = 1302;
		public override uint MessageId
		{
			get
			{
				return 1302;
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
