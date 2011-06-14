// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GameFightMutantInformations.xml' the '14/06/2011 11:32:46'
using System;
using Stump.BaseCore.Framework.IO;

namespace Stump.DofusProtocol.Types
{
	public class GameFightMutantInformations : GameFightFighterNamedInformations
	{
		public const uint Id = 50;
		public short TypeId
		{
			get
			{
				return 50;
			}
		}
		
		public byte powerLevel;
		
		public GameFightMutantInformations()
		{
		}
		
		public GameFightMutantInformations(int contextualId, Types.EntityLook look, Types.EntityDispositionInformations disposition, byte teamId, bool alive, Types.GameFightMinimalStats stats, string name, byte powerLevel)
			 : base(contextualId, look, disposition, teamId, alive, stats, name)
		{
			this.powerLevel = powerLevel;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteByte(powerLevel);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			powerLevel = reader.ReadByte();
			if ( powerLevel < 0 )
			{
				throw new Exception("Forbidden value on powerLevel = " + powerLevel + ", it doesn't respect the following condition : powerLevel < 0");
			}
		}
	}
}
