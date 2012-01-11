// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'AchievementUpdateMessage.xml' the '09/12/2011 21:48:24'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class AchievementUpdateMessage : Message
	{
		public const uint Id = 6207;
		public override uint MessageId
		{
			get
			{
				return 6207;
			}
		}
		
		public Types.Achievement achievement;
		
		public AchievementUpdateMessage()
		{
		}
		
		public AchievementUpdateMessage(Types.Achievement achievement)
		{
			this.achievement = achievement;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			achievement.Serialize(writer);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			achievement = new Types.Achievement();
			achievement.Deserialize(reader);
		}
	}
}
