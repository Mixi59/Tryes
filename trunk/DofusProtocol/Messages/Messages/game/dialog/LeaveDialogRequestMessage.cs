// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'LeaveDialogRequestMessage.xml' the '24/01/2012 22:50:46'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class LeaveDialogRequestMessage : Message
	{
		public const uint Id = 5501;
		public override uint MessageId
		{
			get
			{
				return 5501;
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
