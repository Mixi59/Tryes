// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'CharactersListWithModificationsMessage.xml' the '24/01/2012 22:50:39'
using System;
using Stump.Core.IO;
using System.Collections.Generic;
using System.Linq;

namespace Stump.DofusProtocol.Messages
{
	public class CharactersListWithModificationsMessage : CharactersListMessage
	{
		public const uint Id = 6120;
		public override uint MessageId
		{
			get
			{
				return 6120;
			}
		}
		
		public IEnumerable<Types.CharacterToRecolorInformation> charactersToRecolor;
		public IEnumerable<int> charactersToRename;
		public IEnumerable<int> unusableCharacters;
		
		public CharactersListWithModificationsMessage()
		{
		}
		
		public CharactersListWithModificationsMessage(bool hasStartupActions, IEnumerable<Types.CharacterBaseInformations> characters, IEnumerable<Types.CharacterToRecolorInformation> charactersToRecolor, IEnumerable<int> charactersToRename, IEnumerable<int> unusableCharacters)
			 : base(hasStartupActions, characters)
		{
			this.charactersToRecolor = charactersToRecolor;
			this.charactersToRename = charactersToRename;
			this.unusableCharacters = unusableCharacters;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteUShort((ushort)charactersToRecolor.Count());
			foreach (var entry in charactersToRecolor)
			{
				writer.WriteShort(entry.TypeId);
				entry.Serialize(writer);
			}
			writer.WriteUShort((ushort)charactersToRename.Count());
			foreach (var entry in charactersToRename)
			{
				writer.WriteInt(entry);
			}
			writer.WriteUShort((ushort)unusableCharacters.Count());
			foreach (var entry in unusableCharacters)
			{
				writer.WriteInt(entry);
			}
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			int limit = reader.ReadUShort();
			charactersToRecolor = new Types.CharacterToRecolorInformation[limit];
			for (int i = 0; i < limit; i++)
			{
				(charactersToRecolor as Types.CharacterToRecolorInformation[])[i] = Types.ProtocolTypeManager.GetInstance<Types.CharacterToRecolorInformation>(reader.ReadShort());
				(charactersToRecolor as Types.CharacterToRecolorInformation[])[i].Deserialize(reader);
			}
			limit = reader.ReadUShort();
			charactersToRename = new int[limit];
			for (int i = 0; i < limit; i++)
			{
				(charactersToRename as int[])[i] = reader.ReadInt();
			}
			limit = reader.ReadUShort();
			unusableCharacters = new int[limit];
			for (int i = 0; i < limit; i++)
			{
				(unusableCharacters as int[])[i] = reader.ReadInt();
			}
		}
	}
}
