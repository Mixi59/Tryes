// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GameActionFightSpellCooldownVariationMessage.xml' the '15/06/2011 01:38:41'
using System;
using Stump.BaseCore.Framework.IO;

namespace Stump.DofusProtocol.Messages
{
	public class GameActionFightSpellCooldownVariationMessage : AbstractGameActionMessage
	{
		public const uint Id = 6219;
		public override uint MessageId
		{
			get
			{
				return 6219;
			}
		}
		
		public int targetId;
		public int spellId;
		public short value;
		
		public GameActionFightSpellCooldownVariationMessage()
		{
		}
		
		public GameActionFightSpellCooldownVariationMessage(short actionId, int sourceId, int targetId, int spellId, short value)
			 : base(actionId, sourceId)
		{
			this.targetId = targetId;
			this.spellId = spellId;
			this.value = value;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteInt(targetId);
			writer.WriteInt(spellId);
			writer.WriteShort(value);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			targetId = reader.ReadInt();
			spellId = reader.ReadInt();
			if ( spellId < 0 )
			{
				throw new Exception("Forbidden value on spellId = " + spellId + ", it doesn't respect the following condition : spellId < 0");
			}
			value = reader.ReadShort();
		}
	}
}
