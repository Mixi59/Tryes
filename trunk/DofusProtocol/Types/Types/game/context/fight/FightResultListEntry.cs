// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'FightResultListEntry.xml' the '24/01/2012 22:50:54'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
	public class FightResultListEntry
	{
		public const uint Id = 16;
		public virtual short TypeId
		{
			get
			{
				return 16;
			}
		}
		
		public short outcome;
		public Types.FightLoot rewards;
		
		public FightResultListEntry()
		{
		}
		
		public FightResultListEntry(short outcome, Types.FightLoot rewards)
		{
			this.outcome = outcome;
			this.rewards = rewards;
		}
		
		public virtual void Serialize(IDataWriter writer)
		{
			writer.WriteShort(outcome);
			rewards.Serialize(writer);
		}
		
		public virtual void Deserialize(IDataReader reader)
		{
			outcome = reader.ReadShort();
			if ( outcome < 0 )
			{
				throw new Exception("Forbidden value on outcome = " + outcome + ", it doesn't respect the following condition : outcome < 0");
			}
			rewards = new Types.FightLoot();
			rewards.Deserialize(reader);
		}
	}
}
