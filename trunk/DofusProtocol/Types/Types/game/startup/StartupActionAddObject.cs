// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'StartupActionAddObject.xml' the '14/06/2011 11:32:50'
using System;
using Stump.BaseCore.Framework.IO;

namespace Stump.DofusProtocol.Types
{
	public class StartupActionAddObject
	{
		public const uint Id = 52;
		public short TypeId
		{
			get
			{
				return 52;
			}
		}
		
		public int uid;
		public string title;
		public string text;
		public string descUrl;
		public string pictureUrl;
		public Types.ObjectItemMinimalInformation[] items;
		
		public StartupActionAddObject()
		{
		}
		
		public StartupActionAddObject(int uid, string title, string text, string descUrl, string pictureUrl, Types.ObjectItemMinimalInformation[] items)
		{
			this.uid = uid;
			this.title = title;
			this.text = text;
			this.descUrl = descUrl;
			this.pictureUrl = pictureUrl;
			this.items = items;
		}
		
		public virtual void Serialize(IDataWriter writer)
		{
			writer.WriteInt(uid);
			writer.WriteUTF(title);
			writer.WriteUTF(text);
			writer.WriteUTF(descUrl);
			writer.WriteUTF(pictureUrl);
			writer.WriteUShort((ushort)items.Length);
			for (int i = 0; i < items.Length; i++)
			{
				items[i].Serialize(writer);
			}
		}
		
		public virtual void Deserialize(IDataReader reader)
		{
			uid = reader.ReadInt();
			if ( uid < 0 )
			{
				throw new Exception("Forbidden value on uid = " + uid + ", it doesn't respect the following condition : uid < 0");
			}
			title = reader.ReadUTF();
			text = reader.ReadUTF();
			descUrl = reader.ReadUTF();
			pictureUrl = reader.ReadUTF();
			int limit = reader.ReadUShort();
			items = new Types.ObjectItemMinimalInformation[limit];
			for (int i = 0; i < limit; i++)
			{
				items[i] = new Types.ObjectItemMinimalInformation();
				items[i].Deserialize(reader);
			}
		}
	}
}
