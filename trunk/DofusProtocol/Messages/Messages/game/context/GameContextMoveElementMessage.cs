// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GameContextMoveElementMessage.xml' the '09/12/2011 21:48:27'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class GameContextMoveElementMessage : Message
	{
		public const uint Id = 253;
		public override uint MessageId
		{
			get
			{
				return 253;
			}
		}
		
		public Types.EntityMovementInformations movement;
		
		public GameContextMoveElementMessage()
		{
		}
		
		public GameContextMoveElementMessage(Types.EntityMovementInformations movement)
		{
			this.movement = movement;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			movement.Serialize(writer);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			movement = new Types.EntityMovementInformations();
			movement.Deserialize(reader);
		}
	}
}
