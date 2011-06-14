// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GameFightShowFighterMessage.xml' the '15/06/2011 01:38:49'
using System;
using Stump.BaseCore.Framework.IO;

namespace Stump.DofusProtocol.Messages
{
	public class GameFightShowFighterMessage : Message
	{
		public const uint Id = 5864;
		public override uint MessageId
		{
			get
			{
				return 5864;
			}
		}
		
		public Types.GameFightFighterInformations informations;
		
		public GameFightShowFighterMessage()
		{
		}
		
		public GameFightShowFighterMessage(Types.GameFightFighterInformations informations)
		{
			this.informations = informations;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteShort(informations.TypeId);
			informations.Serialize(writer);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			informations = Types.ProtocolTypeManager.GetInstance<Types.GameFightFighterInformations>(reader.ReadShort());
			informations.Deserialize(reader);
		}
	}
}
