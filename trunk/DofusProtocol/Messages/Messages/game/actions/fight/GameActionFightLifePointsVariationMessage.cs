// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GameActionFightLifePointsVariationMessage.xml' the '09/12/2011 21:48:25'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class GameActionFightLifePointsVariationMessage : AbstractGameActionMessage
	{
		public const uint Id = 5598;
		public override uint MessageId
		{
			get
			{
				return 5598;
			}
		}
		
		public int targetId;
		public short delta;
		
		public GameActionFightLifePointsVariationMessage()
		{
		}
		
		public GameActionFightLifePointsVariationMessage(short actionId, int sourceId, int targetId, short delta)
			 : base(actionId, sourceId)
		{
			this.targetId = targetId;
			this.delta = delta;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteInt(targetId);
			writer.WriteShort(delta);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			targetId = reader.ReadInt();
			delta = reader.ReadShort();
		}
	}
}
