// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'LifePointsRegenBeginMessage.xml' the '09/12/2011 21:48:26'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class LifePointsRegenBeginMessage : Message
	{
		public const uint Id = 5684;
		public override uint MessageId
		{
			get
			{
				return 5684;
			}
		}
		
		public byte regenRate;
		
		public LifePointsRegenBeginMessage()
		{
		}
		
		public LifePointsRegenBeginMessage(byte regenRate)
		{
			this.regenRate = regenRate;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteByte(regenRate);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			regenRate = reader.ReadByte();
			if ( regenRate < 0 || regenRate > 255 )
			{
				throw new Exception("Forbidden value on regenRate = " + regenRate + ", it doesn't respect the following condition : regenRate < 0 || regenRate > 255");
			}
		}
	}
}
