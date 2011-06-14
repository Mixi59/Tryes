// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GameActionFightSpellCastMessage.xml' the '15/06/2011 01:38:41'
using System;
using Stump.BaseCore.Framework.IO;

namespace Stump.DofusProtocol.Messages
{
	public class GameActionFightSpellCastMessage : AbstractGameActionFightTargetedAbilityMessage
	{
		public const uint Id = 1010;
		public override uint MessageId
		{
			get
			{
				return 1010;
			}
		}
		
		public short spellId;
		public byte spellLevel;
		
		public GameActionFightSpellCastMessage()
		{
		}
		
		public GameActionFightSpellCastMessage(short actionId, int sourceId, short destinationCellId, byte critical, bool silentCast, short spellId, byte spellLevel)
			 : base(actionId, sourceId, destinationCellId, critical, silentCast)
		{
			this.spellId = spellId;
			this.spellLevel = spellLevel;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteShort(spellId);
			writer.WriteByte(spellLevel);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			spellId = reader.ReadShort();
			if ( spellId < 0 )
			{
				throw new Exception("Forbidden value on spellId = " + spellId + ", it doesn't respect the following condition : spellId < 0");
			}
			spellLevel = reader.ReadByte();
			if ( spellLevel < 1 || spellLevel > 6 )
			{
				throw new Exception("Forbidden value on spellLevel = " + spellLevel + ", it doesn't respect the following condition : spellLevel < 1 || spellLevel > 6");
			}
		}
	}
}
