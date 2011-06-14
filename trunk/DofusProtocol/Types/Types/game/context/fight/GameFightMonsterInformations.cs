// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GameFightMonsterInformations.xml' the '14/06/2011 11:32:46'
using System;
using Stump.BaseCore.Framework.IO;

namespace Stump.DofusProtocol.Types
{
	public class GameFightMonsterInformations : GameFightAIInformations
	{
		public const uint Id = 29;
		public short TypeId
		{
			get
			{
				return 29;
			}
		}
		
		public short creatureGenericId;
		public byte creatureGrade;
		
		public GameFightMonsterInformations()
		{
		}
		
		public GameFightMonsterInformations(int contextualId, Types.EntityLook look, Types.EntityDispositionInformations disposition, byte teamId, bool alive, Types.GameFightMinimalStats stats, short creatureGenericId, byte creatureGrade)
			 : base(contextualId, look, disposition, teamId, alive, stats)
		{
			this.creatureGenericId = creatureGenericId;
			this.creatureGrade = creatureGrade;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteShort(creatureGenericId);
			writer.WriteByte(creatureGrade);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			creatureGenericId = reader.ReadShort();
			if ( creatureGenericId < 0 )
			{
				throw new Exception("Forbidden value on creatureGenericId = " + creatureGenericId + ", it doesn't respect the following condition : creatureGenericId < 0");
			}
			creatureGrade = reader.ReadByte();
			if ( creatureGrade < 0 )
			{
				throw new Exception("Forbidden value on creatureGrade = " + creatureGrade + ", it doesn't respect the following condition : creatureGrade < 0");
			}
		}
	}
}
