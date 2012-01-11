// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'SpellForgetUIMessage.xml' the '09/12/2011 21:48:32'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class SpellForgetUIMessage : Message
	{
		public const uint Id = 5565;
		public override uint MessageId
		{
			get
			{
				return 5565;
			}
		}
		
		public bool open;
		
		public SpellForgetUIMessage()
		{
		}
		
		public SpellForgetUIMessage(bool open)
		{
			this.open = open;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteBoolean(open);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			open = reader.ReadBoolean();
		}
	}
}
