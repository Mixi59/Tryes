// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'EmotePlayMassiveMessage.xml' the '15/06/2011 01:38:51'
using System;
using Stump.BaseCore.Framework.IO;

namespace Stump.DofusProtocol.Messages
{
	public class EmotePlayMassiveMessage : EmotePlayAbstractMessage
	{
		public const uint Id = 5691;
		public override uint MessageId
		{
			get
			{
				return 5691;
			}
		}
		
		public int[] actorIds;
		
		public EmotePlayMassiveMessage()
		{
		}
		
		public EmotePlayMassiveMessage(byte emoteId, byte duration, int[] actorIds)
			 : base(emoteId, duration)
		{
			this.actorIds = actorIds;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteUShort((ushort)actorIds.Length);
			for (int i = 0; i < actorIds.Length; i++)
			{
				writer.WriteInt(actorIds[i]);
			}
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			int limit = reader.ReadUShort();
			actorIds = new int[limit];
			for (int i = 0; i < limit; i++)
			{
				actorIds[i] = reader.ReadInt();
			}
		}
	}
}
