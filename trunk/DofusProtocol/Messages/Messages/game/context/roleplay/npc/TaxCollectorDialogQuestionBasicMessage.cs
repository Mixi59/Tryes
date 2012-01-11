// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'TaxCollectorDialogQuestionBasicMessage.xml' the '09/12/2011 21:48:30'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class TaxCollectorDialogQuestionBasicMessage : Message
	{
		public const uint Id = 5619;
		public override uint MessageId
		{
			get
			{
				return 5619;
			}
		}
		
		public Types.BasicGuildInformations guildInfo;
		
		public TaxCollectorDialogQuestionBasicMessage()
		{
		}
		
		public TaxCollectorDialogQuestionBasicMessage(Types.BasicGuildInformations guildInfo)
		{
			this.guildInfo = guildInfo;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			guildInfo.Serialize(writer);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			guildInfo = new Types.BasicGuildInformations();
			guildInfo.Deserialize(reader);
		}
	}
}
