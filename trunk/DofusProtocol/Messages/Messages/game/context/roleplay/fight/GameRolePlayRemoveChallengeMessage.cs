// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GameRolePlayRemoveChallengeMessage.xml' the '24/01/2012 22:50:43'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class GameRolePlayRemoveChallengeMessage : Message
	{
		public const uint Id = 300;
		public override uint MessageId
		{
			get
			{
				return 300;
			}
		}
		
		public int fightId;
		
		public GameRolePlayRemoveChallengeMessage()
		{
		}
		
		public GameRolePlayRemoveChallengeMessage(int fightId)
		{
			this.fightId = fightId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteInt(fightId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			fightId = reader.ReadInt();
		}
	}
}
