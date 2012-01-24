// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'FriendAddFailureMessage.xml' the '24/01/2012 22:50:46'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class FriendAddFailureMessage : Message
	{
		public const uint Id = 5600;
		public override uint MessageId
		{
			get
			{
				return 5600;
			}
		}
		
		public sbyte reason;
		
		public FriendAddFailureMessage()
		{
		}
		
		public FriendAddFailureMessage(sbyte reason)
		{
			this.reason = reason;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteSByte(reason);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			reason = reader.ReadSByte();
			if ( reason < 0 )
			{
				throw new Exception("Forbidden value on reason = " + reason + ", it doesn't respect the following condition : reason < 0");
			}
		}
	}
}
