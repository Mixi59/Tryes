// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'JobCrafterDirectorySettingsMessage.xml' the '24/01/2012 22:50:43'
using System;
using Stump.Core.IO;
using System.Collections.Generic;
using System.Linq;

namespace Stump.DofusProtocol.Messages
{
	public class JobCrafterDirectorySettingsMessage : Message
	{
		public const uint Id = 5652;
		public override uint MessageId
		{
			get
			{
				return 5652;
			}
		}
		
		public IEnumerable<Types.JobCrafterDirectorySettings> craftersSettings;
		
		public JobCrafterDirectorySettingsMessage()
		{
		}
		
		public JobCrafterDirectorySettingsMessage(IEnumerable<Types.JobCrafterDirectorySettings> craftersSettings)
		{
			this.craftersSettings = craftersSettings;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteUShort((ushort)craftersSettings.Count());
			foreach (var entry in craftersSettings)
			{
				entry.Serialize(writer);
			}
		}
		
		public override void Deserialize(IDataReader reader)
		{
			int limit = reader.ReadUShort();
			craftersSettings = new Types.JobCrafterDirectorySettings[limit];
			for (int i = 0; i < limit; i++)
			{
				(craftersSettings as Types.JobCrafterDirectorySettings[])[i] = new Types.JobCrafterDirectorySettings();
				(craftersSettings as Types.JobCrafterDirectorySettings[])[i].Deserialize(reader);
			}
		}
	}
}
