// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GameRolePlayMutantInformations.xml' the '14/06/2011 11:32:47'
using System;
using Stump.BaseCore.Framework.IO;

namespace Stump.DofusProtocol.Types
{
	public class GameRolePlayMutantInformations : GameRolePlayHumanoidInformations
	{
		public const uint Id = 3;
		public short TypeId
		{
			get
			{
				return 3;
			}
		}
		
		public int monsterId;
		public byte powerLevel;
		
		public GameRolePlayMutantInformations()
		{
		}
		
		public GameRolePlayMutantInformations(int contextualId, Types.EntityLook look, Types.EntityDispositionInformations disposition, string name, Types.HumanInformations humanoidInfo, int monsterId, byte powerLevel)
			 : base(contextualId, look, disposition, name, humanoidInfo)
		{
			this.monsterId = monsterId;
			this.powerLevel = powerLevel;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteInt(monsterId);
			writer.WriteByte(powerLevel);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			monsterId = reader.ReadInt();
			powerLevel = reader.ReadByte();
		}
	}
}
