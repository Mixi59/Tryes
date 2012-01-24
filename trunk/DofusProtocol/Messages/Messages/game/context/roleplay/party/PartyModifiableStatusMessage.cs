// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'PartyModifiableStatusMessage.xml' the '24/01/2012 22:50:45'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class PartyModifiableStatusMessage : AbstractPartyMessage
	{
		public const uint Id = 6277;
		public override uint MessageId
		{
			get
			{
				return 6277;
			}
		}
		
		public bool enabled;
		
		public PartyModifiableStatusMessage()
		{
		}
		
		public PartyModifiableStatusMessage(int partyId, bool enabled)
			 : base(partyId)
		{
			this.enabled = enabled;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteBoolean(enabled);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			enabled = reader.ReadBoolean();
		}
	}
}
