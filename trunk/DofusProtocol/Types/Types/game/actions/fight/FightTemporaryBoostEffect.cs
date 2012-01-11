// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'FightTemporaryBoostEffect.xml' the '09/12/2011 21:48:38'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
	public class FightTemporaryBoostEffect : AbstractFightDispellableEffect
	{
		public const uint Id = 209;
		public override short TypeId
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
		
		public FightTemporaryBoostEffect(int uid, int targetId, short turnDuration, sbyte dispelable, short spellId, int parentBoostUid, short delta)
			 : base(uid, targetId, turnDuration, dispelable, spellId, parentBoostUid)
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
