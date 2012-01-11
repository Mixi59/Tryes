// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'PartyInvitationDungeonRequestMessage.xml' the '09/12/2011 21:48:31'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class PartyInvitationDungeonRequestMessage : PartyInvitationRequestMessage
	{
		public const uint Id = 6245;
		public override uint MessageId
		{
			get
			{
				return 6245;
			}
		}
		
		public short dungeonId;
		
		public PartyInvitationDungeonRequestMessage()
		{
		}
		
		public PartyInvitationDungeonRequestMessage(string name, short dungeonId)
			 : base(name)
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
