// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'SpellItemBoostMessage.xml' the '24/01/2012 22:50:46'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class SpellItemBoostMessage : Message
	{
		public const uint Id = 6011;
		public override uint MessageId
		{
			get
			{
				return 6011;
			}
		}
		
		public int statId;
		public short spellId;
		public short value;
		
		public SpellItemBoostMessage()
		{
		}
		
		public SpellItemBoostMessage(int statId, short spellId, short value)
		{
			this.statId = statId;
			this.spellId = spellId;
			this.value = value;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteInt(statId);
			writer.WriteShort(spellId);
			writer.WriteShort(value);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			statId = reader.ReadInt();
			if ( statId < 0 )
			{
				throw new Exception("Forbidden value on statId = " + statId + ", it doesn't respect the following condition : statId < 0");
			}
			spellId = reader.ReadShort();
			if ( spellId < 0 )
			{
				throw new Exception("Forbidden value on spellId = " + spellId + ", it doesn't respect the following condition : spellId < 0");
			}
			value = reader.ReadShort();
		}
	}
}
