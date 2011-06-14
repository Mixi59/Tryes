// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'AbstractGameActionFightTargetedAbilityMessage.xml' the '15/06/2011 01:38:40'
using System;
using Stump.BaseCore.Framework.IO;

namespace Stump.DofusProtocol.Messages
{
	public class AbstractGameActionFightTargetedAbilityMessage : AbstractGameActionMessage
	{
		public const uint Id = 6118;
		public override uint MessageId
		{
			get
			{
				return 6118;
			}
		}
		
		public short destinationCellId;
		public byte critical;
		public bool silentCast;
		
		public AbstractGameActionFightTargetedAbilityMessage()
		{
		}
		
		public AbstractGameActionFightTargetedAbilityMessage(short actionId, int sourceId, short destinationCellId, byte critical, bool silentCast)
			 : base(actionId, sourceId)
		{
			this.destinationCellId = destinationCellId;
			this.critical = critical;
			this.silentCast = silentCast;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteShort(destinationCellId);
			writer.WriteByte(critical);
			writer.WriteBoolean(silentCast);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			destinationCellId = reader.ReadShort();
			if ( destinationCellId < -1 || destinationCellId > 559 )
			{
				throw new Exception("Forbidden value on destinationCellId = " + destinationCellId + ", it doesn't respect the following condition : destinationCellId < -1 || destinationCellId > 559");
			}
			critical = reader.ReadByte();
			if ( critical < 0 )
			{
				throw new Exception("Forbidden value on critical = " + critical + ", it doesn't respect the following condition : critical < 0");
			}
			silentCast = reader.ReadBoolean();
		}
	}
}
