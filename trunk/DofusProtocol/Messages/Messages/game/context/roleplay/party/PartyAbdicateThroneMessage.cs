// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'PartyAbdicateThroneMessage.xml' the '15/06/2011 01:38:54'
using System;
using Stump.BaseCore.Framework.IO;

namespace Stump.DofusProtocol.Messages
{
	public class PartyAbdicateThroneMessage : Message
	{
		public const uint Id = 6080;
		public override uint MessageId
		{
			get
			{
				return 6080;
			}
		}
		
		public int playerId;
		
		public PartyAbdicateThroneMessage()
		{
		}
		
		public PartyAbdicateThroneMessage(int playerId)
		{
			this.playerId = playerId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteInt(playerId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			playerId = reader.ReadInt();
			if ( playerId < 0 )
			{
				throw new Exception("Forbidden value on playerId = " + playerId + ", it doesn't respect the following condition : playerId < 0");
			}
		}
	}
}
