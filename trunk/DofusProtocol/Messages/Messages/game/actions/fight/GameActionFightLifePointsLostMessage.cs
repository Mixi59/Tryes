// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GameActionFightLifePointsLostMessage.xml' the '09/12/2011 21:48:25'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class GameActionFightLifePointsLostMessage : AbstractGameActionMessage
	{
		public const uint Id = 6312;
		public override uint MessageId
		{
			get
			{
				return 6312;
			}
		}
		
		public int targetId;
		public short loss;
		public short permanentDamages;
		
		public GameActionFightLifePointsLostMessage()
		{
		}
		
		public GameActionFightLifePointsLostMessage(short actionId, int sourceId, int targetId, short loss, short permanentDamages)
			 : base(actionId, sourceId)
		{
			this.targetId = targetId;
			this.loss = loss;
			this.permanentDamages = permanentDamages;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteInt(targetId);
			writer.WriteShort(loss);
			writer.WriteShort(permanentDamages);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			targetId = reader.ReadInt();
			loss = reader.ReadShort();
			if ( loss < 0 )
			{
				throw new Exception("Forbidden value on loss = " + loss + ", it doesn't respect the following condition : loss < 0");
			}
			permanentDamages = reader.ReadShort();
			if ( permanentDamages < 0 )
			{
				throw new Exception("Forbidden value on permanentDamages = " + permanentDamages + ", it doesn't respect the following condition : permanentDamages < 0");
			}
		}
	}
}
