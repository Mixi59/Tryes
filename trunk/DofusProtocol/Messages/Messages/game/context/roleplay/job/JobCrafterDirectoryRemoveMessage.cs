// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'JobCrafterDirectoryRemoveMessage.xml' the '09/12/2011 21:48:30'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class JobCrafterDirectoryRemoveMessage : Message
	{
		public const uint Id = 5653;
		public override uint MessageId
		{
			get
			{
				return 5653;
			}
		}
		
		public sbyte jobId;
		public int playerId;
		
		public JobCrafterDirectoryRemoveMessage()
		{
		}
		
		public JobCrafterDirectoryRemoveMessage(sbyte jobId, int playerId)
		{
			this.jobId = jobId;
			this.playerId = playerId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteSByte(jobId);
			writer.WriteInt(playerId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			jobId = reader.ReadSByte();
			if ( jobId < 0 )
			{
				throw new Exception("Forbidden value on jobId = " + jobId + ", it doesn't respect the following condition : jobId < 0");
			}
			playerId = reader.ReadInt();
			if ( playerId < 0 )
			{
				throw new Exception("Forbidden value on playerId = " + playerId + ", it doesn't respect the following condition : playerId < 0");
			}
		}
	}
}
