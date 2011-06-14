// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'EnabledChannelsMessage.xml' the '15/06/2011 01:38:45'
using System;
using Stump.BaseCore.Framework.IO;

namespace Stump.DofusProtocol.Messages
{
	public class EnabledChannelsMessage : Message
	{
		public const uint Id = 892;
		public override uint MessageId
		{
			get
			{
				return 892;
			}
		}
		
		public byte[] channels;
		public byte[] disallowed;
		
		public EnabledChannelsMessage()
		{
		}
		
		public EnabledChannelsMessage(byte[] channels, byte[] disallowed)
		{
			this.channels = channels;
			this.disallowed = disallowed;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteUShort((ushort)channels.Length);
			for (int i = 0; i < channels.Length; i++)
			{
				writer.WriteByte(channels[i]);
			}
			writer.WriteUShort((ushort)disallowed.Length);
			for (int i = 0; i < disallowed.Length; i++)
			{
				writer.WriteByte(disallowed[i]);
			}
		}
		
		public override void Deserialize(IDataReader reader)
		{
			int limit = reader.ReadUShort();
			channels = new byte[limit];
			for (int i = 0; i < limit; i++)
			{
				channels[i] = reader.ReadByte();
			}
			limit = reader.ReadUShort();
			disallowed = new byte[limit];
			for (int i = 0; i < limit; i++)
			{
				disallowed[i] = reader.ReadByte();
			}
		}
	}
}
