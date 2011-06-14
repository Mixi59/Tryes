// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'CharacterSelectedForceMessage.xml' the '15/06/2011 01:38:43'
using System;
using Stump.BaseCore.Framework.IO;

namespace Stump.DofusProtocol.Messages
{
	public class CharacterSelectedForceMessage : Message
	{
		public const uint Id = 6068;
		public override uint MessageId
		{
			get
			{
				return 6068;
			}
		}
		
		public int id;
		
		public CharacterSelectedForceMessage()
		{
		}
		
		public CharacterSelectedForceMessage(int id)
		{
			this.id = id;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteInt(id);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			id = reader.ReadInt();
			if ( id < 1 || id > 2147483647 )
			{
				throw new Exception("Forbidden value on id = " + id + ", it doesn't respect the following condition : id < 1 || id > 2147483647");
			}
		}
	}
}
