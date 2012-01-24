// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'PartyInvitationDungeonMessage.xml' the '24/01/2012 22:50:45'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class PartyInvitationDungeonMessage : PartyInvitationMessage
	{
		public const uint Id = 6244;
		public override uint MessageId
		{
			get
			{
				return 6244;
			}
		}
		
		public short dungeonId;
		
		public PartyInvitationDungeonMessage()
		{
		}
		
		public PartyInvitationDungeonMessage(int partyId, sbyte partyType, sbyte maxParticipants, int fromId, string fromName, int toId, short dungeonId)
			 : base(partyId, partyType, maxParticipants, fromId, fromName, toId)
		{
			this.dungeonId = dungeonId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteShort(dungeonId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			dungeonId = reader.ReadShort();
			if ( dungeonId < 0 )
			{
				throw new Exception("Forbidden value on dungeonId = " + dungeonId + ", it doesn't respect the following condition : dungeonId < 0");
			}
		}
	}
}
