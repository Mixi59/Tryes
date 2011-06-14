// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'PackRestrictedSubAreaMessage.xml' the '15/06/2011 01:39:08'
using System;
using Stump.BaseCore.Framework.IO;

namespace Stump.DofusProtocol.Messages
{
	public class PackRestrictedSubAreaMessage : Message
	{
		public const uint Id = 6186;
		public override uint MessageId
		{
			get
			{
				return 6186;
			}
		}
		
		public int subAreaId;
		
		public PackRestrictedSubAreaMessage()
		{
		}
		
		public PackRestrictedSubAreaMessage(int subAreaId)
		{
			this.subAreaId = subAreaId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteInt(subAreaId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			subAreaId = reader.ReadInt();
			if ( subAreaId < 0 )
			{
				throw new Exception("Forbidden value on subAreaId = " + subAreaId + ", it doesn't respect the following condition : subAreaId < 0");
			}
		}
	}
}
