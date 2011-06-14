// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'InventoryPresetSaveResultMessage.xml' the '15/06/2011 01:39:07'
using System;
using Stump.BaseCore.Framework.IO;

namespace Stump.DofusProtocol.Messages
{
	public class InventoryPresetSaveResultMessage : Message
	{
		public const uint Id = 6170;
		public override uint MessageId
		{
			get
			{
				return 6170;
			}
		}
		
		public byte presetId;
		public byte code;
		
		public InventoryPresetSaveResultMessage()
		{
		}
		
		public InventoryPresetSaveResultMessage(byte presetId, byte code)
		{
			this.presetId = presetId;
			this.code = code;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteByte(presetId);
			writer.WriteByte(code);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			presetId = reader.ReadByte();
			if ( presetId < 0 )
			{
				throw new Exception("Forbidden value on presetId = " + presetId + ", it doesn't respect the following condition : presetId < 0");
			}
			code = reader.ReadByte();
			if ( code < 0 )
			{
				throw new Exception("Forbidden value on code = " + code + ", it doesn't respect the following condition : code < 0");
			}
		}
	}
}
