// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GameFightMinimalStatsPreparation.xml' the '24/01/2012 22:50:54'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
	public class GameFightMinimalStatsPreparation : GameFightMinimalStats
	{
		public const uint Id = 360;
		public override short TypeId
		{
			get
			{
				return 360;
			}
		}
		
		public int initiative;
		
		public GameFightMinimalStatsPreparation()
		{
		}
		
		public GameFightMinimalStatsPreparation(int lifePoints, int maxLifePoints, int baseMaxLifePoints, int permanentDamagePercent, int shieldPoints, short actionPoints, short maxActionPoints, short movementPoints, short maxMovementPoints, int summoner, bool summoned, short neutralElementResistPercent, short earthElementResistPercent, short waterElementResistPercent, short airElementResistPercent, short fireElementResistPercent, short dodgePALostProbability, short dodgePMLostProbability, short tackleBlock, short tackleEvade, sbyte invisibilityState, int initiative)
			 : base(lifePoints, maxLifePoints, baseMaxLifePoints, permanentDamagePercent, shieldPoints, actionPoints, maxActionPoints, movementPoints, maxMovementPoints, summoner, summoned, neutralElementResistPercent, earthElementResistPercent, waterElementResistPercent, airElementResistPercent, fireElementResistPercent, dodgePALostProbability, dodgePMLostProbability, tackleBlock, tackleEvade, invisibilityState)
		{
			this.initiative = initiative;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteInt(initiative);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			initiative = reader.ReadInt();
			if ( initiative < 0 )
			{
				throw new Exception("Forbidden value on initiative = " + initiative + ", it doesn't respect the following condition : initiative < 0");
			}
		}
	}
}
