// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'TeleportToBuddyCloseMessage.xml' the '09/12/2011 21:48:34'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class TeleportToBuddyCloseMessage : Message
	{
		public const uint Id = 6303;
		public override uint MessageId
		{
			get
			{
				return 6303;
			}
		}
		
		public short dungeonId;
		public int buddyId;
		
		public TeleportToBuddyCloseMessage()
		{
		}
		
		public TeleportToBuddyCloseMessage(short dungeonId, int buddyId)
		{
			this.dungeonId = dungeonId;
			this.buddyId = buddyId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteShort(dungeonId);
			writer.WriteInt(buddyId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			dungeonId = reader.ReadShort();
			if ( dungeonId < 0 )
			{
				throw new Exception("Forbidden value on dungeonId = " + dungeonId + ", it doesn't respect the following condition : dungeonId < 0");
			}
			buddyId = reader.ReadInt();
			if ( buddyId < 0 )
			{
				throw new Exception("Forbidden value on buddyId = " + buddyId + ", it doesn't respect the following condition : buddyId < 0");
			}
		}
	}
}