// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'SpellMovementMessage.xml' the '15/06/2011 01:38:56'
using System;
using Stump.BaseCore.Framework.IO;

namespace Stump.DofusProtocol.Messages
{
	public class SpellMovementMessage : Message
	{
		public const uint Id = 5746;
		public override uint MessageId
		{
			get
			{
				return 5746;
			}
		}
		
		public short spellId;
		public byte position;
		
		public SpellMovementMessage()
		{
		}
		
		public SpellMovementMessage(short spellId, byte position)
		{
			this.spellId = spellId;
			this.position = position;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteShort(spellId);
			writer.WriteByte(position);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			spellId = reader.ReadShort();
			if ( spellId < 0 )
			{
				throw new Exception("Forbidden value on spellId = " + spellId + ", it doesn't respect the following condition : spellId < 0");
			}
			position = reader.ReadByte();
			if ( position < 63 || position > 255 )
			{
				throw new Exception("Forbidden value on position = " + position + ", it doesn't respect the following condition : position < 63 || position > 255");
			}
		}
	}
}
