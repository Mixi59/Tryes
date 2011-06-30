// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GameRolePlayGroupMonsterInformations.xml' the '30/06/2011 11:40:23'
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
		public byte mainCreatureGrade;
		public IEnumerable<Types.MonsterInGroupInformations> underlings;
		public short ageBonus;
		public byte alignmentSide;
		
		public GameRolePlayGroupMonsterInformations()
		{
		}
		
		public GameRolePlayGroupMonsterInformations(int contextualId, Types.EntityLook look, Types.EntityDispositionInformations disposition, int mainCreatureGenericId, byte mainCreatureGrade, IEnumerable<Types.MonsterInGroupInformations> underlings, short ageBonus, byte alignmentSide)
			 : base(contextualId, look, disposition)
		{
			this.mainCreatureGenericId = mainCreatureGenericId;
			this.mainCreatureGrade = mainCreatureGrade;
			this.underlings = underlings;
			this.ageBonus = ageBonus;
			this.alignmentSide = alignmentSide;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteInt(mainCreatureGenericId);
			writer.WriteByte(mainCreatureGrade);
			writer.WriteUShort((ushort)underlings.Count());
			foreach (var entry in underlings)
			{
				entry.Serialize(writer);
			}
			writer.WriteShort(ageBonus);
			writer.WriteByte(alignmentSide);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			mainCreatureGenericId = reader.ReadInt();
			mainCreatureGrade = reader.ReadByte();
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
			alignmentSide = reader.ReadByte();
		}
	}
}