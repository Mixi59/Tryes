// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'CharacterNameSuggestionRequestMessage.xml' the '15/06/2011 01:38:44'
using System;
using Stump.BaseCore.Framework.IO;

namespace Stump.DofusProtocol.Messages
{
	public class CharacterNameSuggestionRequestMessage : Message
	{
		public const uint Id = 162;
		public override uint MessageId
		{
			get
			{
				return 162;
			}
		}
		
		
		public override void Serialize(IDataWriter writer)
		{
		}
		
		public override void Deserialize(IDataReader reader)
		{
		}
	}
}
