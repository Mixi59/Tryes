// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ExchangeCraftResultWithObjectDescMessage.xml' the '15/06/2011 01:39:02'
using System;
using Stump.BaseCore.Framework.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ExchangeCraftResultWithObjectDescMessage : ExchangeCraftResultMessage
	{
		public const uint Id = 5999;
		public override uint MessageId
		{
			get
			{
				return 5999;
			}
		}
		
		public Types.ObjectItemNotInContainer objectInfo;
		
		public ExchangeCraftResultWithObjectDescMessage()
		{
		}
		
		public ExchangeCraftResultWithObjectDescMessage(byte craftResult, Types.ObjectItemNotInContainer objectInfo)
			 : base(craftResult)
		{
			this.objectInfo = objectInfo;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			objectInfo.Serialize(writer);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			objectInfo = new Types.ObjectItemNotInContainer();
			objectInfo.Deserialize(reader);
		}
	}
}
