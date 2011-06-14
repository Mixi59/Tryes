// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'PartyFollowThisMemberRequestMessage.xml' the '15/06/2011 01:38:54'
using System;
using Stump.BaseCore.Framework.IO;

namespace Stump.DofusProtocol.Messages
{
	public class PartyFollowThisMemberRequestMessage : PartyFollowMemberRequestMessage
	{
		public const uint Id = 5588;
		public override uint MessageId
		{
			get
			{
				return 5588;
			}
		}
		
		public bool enabled;
		
		public PartyFollowThisMemberRequestMessage()
		{
		}
		
		public PartyFollowThisMemberRequestMessage(int playerId, bool enabled)
			 : base(playerId)
		{
			this.enabled = enabled;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteBoolean(enabled);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			enabled = reader.ReadBoolean();
		}
	}
}
