// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GameFightResumeMessage.xml' the '15/06/2011 01:38:47'
using System;
using Stump.BaseCore.Framework.IO;

namespace Stump.DofusProtocol.Messages
{
	public class GameFightResumeMessage : GameFightSpectateMessage
	{
		public const uint Id = 6067;
		public override uint MessageId
		{
			get
			{
				return 6067;
			}
		}
		
		public Types.GameFightSpellCooldown[] spellCooldowns;
		public byte summonCount;
		public byte bombCount;
		
		public GameFightResumeMessage()
		{
		}
		
		public GameFightResumeMessage(Types.FightDispellableEffectExtendedInformations[] effects, Types.GameActionMark[] marks, short gameTurn, Types.GameFightSpellCooldown[] spellCooldowns, byte summonCount, byte bombCount)
			 : base(effects, marks, gameTurn)
		{
			this.spellCooldowns = spellCooldowns;
			this.summonCount = summonCount;
			this.bombCount = bombCount;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteUShort((ushort)spellCooldowns.Length);
			for (int i = 0; i < spellCooldowns.Length; i++)
			{
				spellCooldowns[i].Serialize(writer);
			}
			writer.WriteByte(summonCount);
			writer.WriteByte(bombCount);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			int limit = reader.ReadUShort();
			spellCooldowns = new Types.GameFightSpellCooldown[limit];
			for (int i = 0; i < limit; i++)
			{
				spellCooldowns[i] = new Types.GameFightSpellCooldown();
				spellCooldowns[i].Deserialize(reader);
			}
			summonCount = reader.ReadByte();
			if ( summonCount < 0 )
			{
				throw new Exception("Forbidden value on summonCount = " + summonCount + ", it doesn't respect the following condition : summonCount < 0");
			}
			bombCount = reader.ReadByte();
			if ( bombCount < 0 )
			{
				throw new Exception("Forbidden value on bombCount = " + bombCount + ", it doesn't respect the following condition : bombCount < 0");
			}
		}
	}
}
