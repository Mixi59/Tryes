// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ConsoleCommandsListMessage.xml' the '15/06/2011 01:38:38'
using System;
using Stump.BaseCore.Framework.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ConsoleCommandsListMessage : Message
	{
		public const uint Id = 6127;
		public override uint MessageId
		{
			get
			{
				return 6127;
			}
		}
		
		public string[] aliases;
		public string[] arguments;
		public string[] descriptions;
		
		public ConsoleCommandsListMessage()
		{
		}
		
		public ConsoleCommandsListMessage(string[] aliases, string[] arguments, string[] descriptions)
		{
			this.aliases = aliases;
			this.arguments = arguments;
			this.descriptions = descriptions;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteUShort((ushort)aliases.Length);
			for (int i = 0; i < aliases.Length; i++)
			{
				writer.WriteUTF(aliases[i]);
			}
			writer.WriteUShort((ushort)arguments.Length);
			for (int i = 0; i < arguments.Length; i++)
			{
				writer.WriteUTF(arguments[i]);
			}
			writer.WriteUShort((ushort)descriptions.Length);
			for (int i = 0; i < descriptions.Length; i++)
			{
				writer.WriteUTF(descriptions[i]);
			}
		}
		
		public override void Deserialize(IDataReader reader)
		{
			int limit = reader.ReadUShort();
			aliases = new string[limit];
			for (int i = 0; i < limit; i++)
			{
				aliases[i] = reader.ReadUTF();
			}
			limit = reader.ReadUShort();
			arguments = new string[limit];
			for (int i = 0; i < limit; i++)
			{
				arguments[i] = reader.ReadUTF();
			}
			limit = reader.ReadUShort();
			descriptions = new string[limit];
			for (int i = 0; i < limit; i++)
			{
				descriptions[i] = reader.ReadUTF();
			}
		}
	}
}
