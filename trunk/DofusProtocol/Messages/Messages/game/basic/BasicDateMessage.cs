// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'BasicDateMessage.xml' the '24/01/2012 22:50:39'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class BasicDateMessage : Message
	{
		public const uint Id = 177;
		public override uint MessageId
		{
			get
			{
				return 177;
			}
		}
		
		public sbyte day;
		public sbyte month;
		public short year;
		
		public BasicDateMessage()
		{
		}
		
		public BasicDateMessage(sbyte day, sbyte month, short year)
		{
			this.day = day;
			this.month = month;
			this.year = year;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteSByte(day);
			writer.WriteSByte(month);
			writer.WriteShort(year);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			day = reader.ReadSByte();
			if ( day < 0 )
			{
				throw new Exception("Forbidden value on day = " + day + ", it doesn't respect the following condition : day < 0");
			}
			month = reader.ReadSByte();
			if ( month < 0 )
			{
				throw new Exception("Forbidden value on month = " + month + ", it doesn't respect the following condition : month < 0");
			}
			year = reader.ReadShort();
			if ( year < 0 )
			{
				throw new Exception("Forbidden value on year = " + year + ", it doesn't respect the following condition : year < 0");
			}
		}
	}
}
