// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'JobUnlearntMessage.xml' the '15/06/2011 01:38:52'
using System;
using Stump.BaseCore.Framework.IO;

namespace Stump.DofusProtocol.Messages
{
	public class JobUnlearntMessage : Message
	{
		public const uint Id = 5657;
		public override uint MessageId
		{
			get
			{
				return 5657;
			}
		}
		
		public byte jobId;
		
		public JobUnlearntMessage()
		{
		}
		
		public JobUnlearntMessage(byte jobId)
		{
			this.jobId = jobId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteByte(jobId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			jobId = reader.ReadByte();
			if ( jobId < 0 )
			{
				throw new Exception("Forbidden value on jobId = " + jobId + ", it doesn't respect the following condition : jobId < 0");
			}
		}
	}
}
