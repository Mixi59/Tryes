// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'FightTemporaryBoostEffect.xml' the '14/06/2011 11:32:44'
using System;
using Stump.BaseCore.Framework.IO;

namespace Stump.DofusProtocol.Types
{
	public class FightTemporaryBoostEffect : AbstractFightDispellableEffect
	{
		public const uint Id = 209;
		public short TypeId
		{
			get
			{
				return 209;
			}
		}
		
		public short delta;
		
		public FightTemporaryBoostEffect()
		{
		}
		
		public FightTemporaryBoostEffect(int uid, int targetId, short turnDuration, byte dispelable, short spellId, short delta)
			 : base(uid, targetId, turnDuration, dispelable, spellId)
		{
			this.delta = delta;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteShort(delta);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			delta = reader.ReadShort();
		}
	}
}
