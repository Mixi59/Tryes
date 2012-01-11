// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'CinematicMessage.xml' the '09/12/2011 21:48:37'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class CinematicMessage : Message
	{
		public const uint Id = 6053;
		public override uint MessageId
		{
			get
			{
				return 6053;
			}
		}
		
		public short cinematicId;
		
		public CinematicMessage()
		{
		}
		
		public CinematicMessage(short cinematicId)
		{
			this.cinematicId = cinematicId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteShort(cinematicId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			cinematicId = reader.ReadShort();
			if ( cinematicId < 0 )
			{
				throw new Exception("Forbidden value on cinematicId = " + cinematicId + ", it doesn't respect the following condition : cinematicId < 0");
			}
		}
	}
}
