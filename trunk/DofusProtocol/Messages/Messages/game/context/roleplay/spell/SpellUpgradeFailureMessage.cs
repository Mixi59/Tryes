// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'SpellUpgradeFailureMessage.xml' the '09/12/2011 21:48:32'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class SpellUpgradeFailureMessage : Message
	{
		public const uint Id = 1202;
		public override uint MessageId
		{
			get
			{
				return 1202;
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
