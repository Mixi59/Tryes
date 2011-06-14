// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GameDataPaddockObjectAddMessage.xml' the '15/06/2011 01:38:49'
using System;
using Stump.BaseCore.Framework.IO;

namespace Stump.DofusProtocol.Messages
{
	public class GameDataPaddockObjectAddMessage : Message
	{
		public const uint Id = 5990;
		public override uint MessageId
		{
			get
			{
				return 5990;
			}
		}
		
		public Types.PaddockItem paddockItemDescription;
		
		public GameDataPaddockObjectAddMessage()
		{
		}
		
		public GameDataPaddockObjectAddMessage(Types.PaddockItem paddockItemDescription)
		{
			this.paddockItemDescription = paddockItemDescription;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			paddockItemDescription.Serialize(writer);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			paddockItemDescription = new Types.PaddockItem();
			paddockItemDescription.Deserialize(reader);
		}
	}
}
