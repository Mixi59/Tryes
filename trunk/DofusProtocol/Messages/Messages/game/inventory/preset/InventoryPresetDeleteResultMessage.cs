// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'InventoryPresetDeleteResultMessage.xml' the '09/12/2011 21:48:36'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class InventoryPresetDeleteResultMessage : Message
	{
		public const uint Id = 6173;
		public override uint MessageId
		{
			get
			{
				return 6173;
			}
		}
		
		public sbyte presetId;
		public sbyte code;
		
		public InventoryPresetDeleteResultMessage()
		{
		}
		
		public InventoryPresetDeleteResultMessage(sbyte presetId, sbyte code)
		{
			this.presetId = presetId;
			this.code = code;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteSByte(presetId);
			writer.WriteSByte(code);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			presetId = reader.ReadSByte();
			if ( presetId < 0 )
			{
				throw new Exception("Forbidden value on presetId = " + presetId + ", it doesn't respect the following condition : presetId < 0");
			}
			code = reader.ReadSByte();
			if ( code < 0 )
			{
				throw new Exception("Forbidden value on code = " + code + ", it doesn't respect the following condition : code < 0");
			}
		}
	}
}
