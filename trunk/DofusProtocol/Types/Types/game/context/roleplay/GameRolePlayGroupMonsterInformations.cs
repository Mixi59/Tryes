// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GameRolePlayGroupMonsterInformations.xml' the '24/01/2012 22:50:54'
using System;
using Stump.Core.IO;
using System.Collections.Generic;
using System.Linq;

namespace Stump.DofusProtocol.Types
{
	public class GameRolePlayGroupMonsterInformations : GameRolePlayActorInformations
	{
		public const uint Id = 160;
		public override short TypeId
		{
			get
			{
				return 160;
			}
		}
		
		public int mainCreatureGenericId;
		public sbyte mainCreatureGrade;
		public IEnumerable<Types.MonsterInGroupInformations> underlings;
		public short ageBonus;
		public sbyte alignmentSide;
		public bool keyRingBonus;
		
		public GameRolePlayGroupMonsterInformations()
		{
		}
		
		public GameRolePlayGroupMonsterInformations(int contextualId, Types.EntityLook look, Types.EntityDispositionInformations disposition, int mainCreatureGenericId, sbyte mainCreatureGrade, IEnumerable<Types.MonsterInGroupInformations> underlings, short ageBonus, sbyte alignmentSide, bool keyRingBonus)
			 : base(contextualId, look, disposition)
		{
			this.mainCreatureGenericId = mainCreatureGenericId;
			this.mainCreatureGrade = mainCreatureGrade;
			this.underlings = underlings;
			this.ageBonus = ageBonus;
			this.alignmentSide = alignmentSide;
			this.keyRingBonus = keyRingBonus;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteInt(mainCreatureGenericId);
			writer.WriteSByte(mainCreatureGrade);
			writer.WriteUShort((ushort)underlings.Count());
			foreach (var entry in underlings)
			{
				entry.Serialize(writer);
			}
			writer.WriteShort(ageBonus);
			writer.WriteSByte(alignmentSide);
			writer.WriteBoolean(keyRingBonus);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			mainCreatureGenericId = reader.ReadInt();
			mainCreatureGrade = reader.ReadSByte();
			if ( mainCreatureGrade < 0 )
			{
				throw new Exception("Forbidden value on mainCreatureGrade = " + mainCreatureGrade + ", it doesn't respect the following condition : mainCreatureGrade < 0");
			}
			int limit = reader.ReadUShort();
			underlings = new Types.MonsterInGroupInformations[limit];
			for (int i = 0; i < limit; i++)
			{
				(underlings as MonsterInGroupInformations[])[i] = new Types.MonsterInGroupInformations();
				(underlings as Types.MonsterInGroupInformations[])[i].Deserialize(reader);
			}
			ageBonus = reader.ReadShort();
			if ( ageBonus < -1 || ageBonus > 1000 )
			{
				throw new Exception("Forbidden value on ageBonus = " + ageBonus + ", it doesn't respect the following condition : ageBonus < -1 || ageBonus > 1000");
			}
			alignmentSide = reader.ReadSByte();
			keyRingBonus = reader.ReadBoolean();
		}
	}
}
