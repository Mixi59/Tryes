// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'FightExternalInformations.xml' the '09/12/2011 21:48:38'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
	public class FightExternalInformations
	{
		public const uint Id = 117;
		public virtual short TypeId
		{
			get
			{
				return 117;
			}
		}
		
		public int fightId;
		public int fightStart;
		public bool fightSpectatorLocked;
		
		public FightExternalInformations()
		{
		}
		
		public FightExternalInformations(int fightId, int fightStart, bool fightSpectatorLocked)
		{
			this.fightId = fightId;
			this.fightStart = fightStart;
			this.fightSpectatorLocked = fightSpectatorLocked;
		}
		
		public virtual void Serialize(IDataWriter writer)
		{
			writer.WriteInt(fightId);
			writer.WriteInt(fightStart);
			writer.WriteBoolean(fightSpectatorLocked);
		}
		
		public virtual void Deserialize(IDataReader reader)
		{
			fightId = reader.ReadInt();
			fightStart = reader.ReadInt();
			if ( fightStart < 0 )
			{
				throw new Exception("Forbidden value on fightStart = " + fightStart + ", it doesn't respect the following condition : fightStart < 0");
			}
			fightSpectatorLocked = reader.ReadBoolean();
		}
	}
}
