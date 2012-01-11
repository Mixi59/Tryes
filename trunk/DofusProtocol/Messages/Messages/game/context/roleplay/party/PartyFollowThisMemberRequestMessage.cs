// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'PartyFollowThisMemberRequestMessage.xml' the '09/12/2011 21:48:31'
using System;
using Stump.Core.IO;

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
		
		public PartyFollowThisMemberRequestMessage(int partyId, int playerId, bool enabled)
			 : base(partyId, playerId)
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
