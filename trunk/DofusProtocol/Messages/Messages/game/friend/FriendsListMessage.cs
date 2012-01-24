// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'FriendsListMessage.xml' the '24/01/2012 22:50:46'
using System;
using Stump.Core.IO;
using System.Collections.Generic;
using System.Linq;

namespace Stump.DofusProtocol.Messages
{
	public class FriendsListMessage : Message
	{
		public const uint Id = 4002;
		public override uint MessageId
		{
			get
			{
				return 4002;
			}
		}
		
		public IEnumerable<Types.FriendInformations> friendsList;
		
		public FriendsListMessage()
		{
		}
		
		public FriendsListMessage(IEnumerable<Types.FriendInformations> friendsList)
		{
			this.friendsList = friendsList;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteUShort((ushort)friendsList.Count());
			foreach (var entry in friendsList)
			{
				writer.WriteShort(entry.TypeId);
				entry.Serialize(writer);
			}
		}
		
		public override void Deserialize(IDataReader reader)
		{
			int limit = reader.ReadUShort();
			friendsList = new Types.FriendInformations[limit];
			for (int i = 0; i < limit; i++)
			{
				(friendsList as Types.FriendInformations[])[i] = Types.ProtocolTypeManager.GetInstance<Types.FriendInformations>(reader.ReadShort());
				(friendsList as Types.FriendInformations[])[i].Deserialize(reader);
			}
		}
	}
}
