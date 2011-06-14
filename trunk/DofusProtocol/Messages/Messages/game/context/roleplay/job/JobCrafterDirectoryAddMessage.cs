// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'JobCrafterDirectoryAddMessage.xml' the '15/06/2011 01:38:52'
using System;
using Stump.BaseCore.Framework.IO;

namespace Stump.DofusProtocol.Messages
{
	public class JobCrafterDirectoryAddMessage : Message
	{
		public const uint Id = 5651;
		public override uint MessageId
		{
			get
			{
				return 5651;
			}
		}
		
		public Types.JobCrafterDirectoryListEntry listEntry;
		
		public JobCrafterDirectoryAddMessage()
		{
		}
		
		public JobCrafterDirectoryAddMessage(Types.JobCrafterDirectoryListEntry listEntry)
		{
			this.listEntry = listEntry;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			listEntry.Serialize(writer);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			listEntry = new Types.JobCrafterDirectoryListEntry();
			listEntry.Deserialize(reader);
		}
	}
}
