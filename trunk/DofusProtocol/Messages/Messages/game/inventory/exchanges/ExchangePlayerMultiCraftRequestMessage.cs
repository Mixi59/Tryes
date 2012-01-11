// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ExchangePlayerMultiCraftRequestMessage.xml' the '09/12/2011 21:48:35'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ExchangePlayerMultiCraftRequestMessage : ExchangeRequestMessage
	{
		public const uint Id = 5784;
		public override uint MessageId
		{
			get
			{
				return 5784;
			}
		}
		
		public int target;
		public int skillId;
		
		public ExchangePlayerMultiCraftRequestMessage()
		{
		}
		
		public ExchangePlayerMultiCraftRequestMessage(sbyte exchangeType, int target, int skillId)
			 : base(exchangeType)
		{
			this.target = target;
			this.skillId = skillId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteInt(target);
			writer.WriteInt(skillId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			target = reader.ReadInt();
			if ( target < 0 )
			{
				throw new Exception("Forbidden value on target = " + target + ", it doesn't respect the following condition : target < 0");
			}
			skillId = reader.ReadInt();
			if ( skillId < 0 )
			{
				throw new Exception("Forbidden value on skillId = " + skillId + ", it doesn't respect the following condition : skillId < 0");
			}
		}
	}
}
