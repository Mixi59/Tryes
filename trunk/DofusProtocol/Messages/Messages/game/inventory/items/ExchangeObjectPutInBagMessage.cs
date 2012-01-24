// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ExchangeObjectPutInBagMessage.xml' the '24/01/2012 22:50:50'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ExchangeObjectPutInBagMessage : ExchangeObjectMessage
	{
		public const uint Id = 6009;
		public override uint MessageId
		{
			get
			{
				return 6009;
			}
		}
		
		public Types.ObjectItem @object;
		
		public ExchangeObjectPutInBagMessage()
		{
		}
		
		public ExchangeObjectPutInBagMessage(bool remote, Types.ObjectItem @object)
			 : base(remote)
		{
			this.@object = @object;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			@object.Serialize(writer);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			@object = new Types.ObjectItem();
			@object.Deserialize(reader);
		}
	}
}
