// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'NpcStaticInformations.xml' the '09/12/2011 21:48:39'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
	public class NpcStaticInformations
	{
		public const uint Id = 155;
		public virtual short TypeId
		{
			get
			{
				return 155;
			}
		}
		
		public short npcId;
		public bool sex;
		public short specialArtworkId;
		
		public NpcStaticInformations()
		{
		}
		
		public NpcStaticInformations(short npcId, bool sex, short specialArtworkId)
		{
			this.npcId = npcId;
			this.sex = sex;
			this.specialArtworkId = specialArtworkId;
		}
		
		public virtual void Serialize(IDataWriter writer)
		{
			writer.WriteShort(npcId);
			writer.WriteBoolean(sex);
			writer.WriteShort(specialArtworkId);
		}
		
		public virtual void Deserialize(IDataReader reader)
		{
			npcId = reader.ReadShort();
			if ( npcId < 0 )
			{
				throw new Exception("Forbidden value on npcId = " + npcId + ", it doesn't respect the following condition : npcId < 0");
			}
			sex = reader.ReadBoolean();
			specialArtworkId = reader.ReadShort();
			if ( specialArtworkId < 0 )
			{
				throw new Exception("Forbidden value on specialArtworkId = " + specialArtworkId + ", it doesn't respect the following condition : specialArtworkId < 0");
			}
		}
	}
}
