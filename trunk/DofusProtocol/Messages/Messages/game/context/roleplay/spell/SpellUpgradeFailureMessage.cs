// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'SpellUpgradeFailureMessage.xml' the '15/06/2011 01:38:56'
using System;
using Stump.BaseCore.Framework.IO;

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
