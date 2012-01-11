// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'CharacterNameSuggestionFailureMessage.xml' the '09/12/2011 21:48:26'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class CharacterNameSuggestionFailureMessage : Message
	{
		public const uint Id = 164;
		public override uint MessageId
		{
			get
			{
				return 164;
			}
		}
		
		public sbyte reason;
		
		public CharacterNameSuggestionFailureMessage()
		{
		}
		
		public CharacterNameSuggestionFailureMessage(sbyte reason)
		{
			this.reason = reason;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteSByte(reason);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			reason = reader.ReadSByte();
			if ( reason < 0 )
			{
				throw new Exception("Forbidden value on reason = " + reason + ", it doesn't respect the following condition : reason < 0");
			}
		}
	}
}
