// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GameRolePlayNamedActorInformations.xml' the '09/12/2011 21:48:39'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
	public class GameRolePlayNamedActorInformations : GameRolePlayActorInformations
	{
		public const uint Id = 154;
		public override short TypeId
		{
			get
			{
				return 154;
			}
		}
		
		public string name;
		
		public GameRolePlayNamedActorInformations()
		{
		}
		
		public GameRolePlayNamedActorInformations(int contextualId, Types.EntityLook look, Types.EntityDispositionInformations disposition, string name)
			 : base(contextualId, look, disposition)
		{
			this.name = name;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteUTF(name);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			name = reader.ReadUTF();
		}
	}
}
