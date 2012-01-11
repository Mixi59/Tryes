// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'QuestObjectiveValidationMessage.xml' the '09/12/2011 21:48:32'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class QuestObjectiveValidationMessage : Message
	{
		public const uint Id = 6085;
		public override uint MessageId
		{
			get
			{
				return 6085;
			}
		}
		
		public short questId;
		public short objectiveId;
		
		public QuestObjectiveValidationMessage()
		{
		}
		
		public QuestObjectiveValidationMessage(short questId, short objectiveId)
		{
			this.questId = questId;
			this.objectiveId = objectiveId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteShort(questId);
			writer.WriteShort(objectiveId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			questId = reader.ReadShort();
			if ( questId < 0 )
			{
				throw new Exception("Forbidden value on questId = " + questId + ", it doesn't respect the following condition : questId < 0");
			}
			objectiveId = reader.ReadShort();
			if ( objectiveId < 0 )
			{
				throw new Exception("Forbidden value on objectiveId = " + objectiveId + ", it doesn't respect the following condition : objectiveId < 0");
			}
		}
	}
}
