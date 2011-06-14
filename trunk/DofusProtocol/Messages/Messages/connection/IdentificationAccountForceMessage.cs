// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'IdentificationAccountForceMessage.xml' the '15/06/2011 01:38:38'
using System;
using Stump.BaseCore.Framework.IO;

namespace Stump.DofusProtocol.Messages
{
	public class IdentificationAccountForceMessage : IdentificationMessage
	{
		public const uint Id = 6119;
		public override uint MessageId
		{
			get
			{
				return 6119;
			}
		}
		
		public string forcedAccountLogin;
		
		public IdentificationAccountForceMessage()
		{
		}
		
		public IdentificationAccountForceMessage(Types.Version version, string login, string password, bool autoconnect, string forcedAccountLogin)
			 : base(version, login, password, autoconnect)
		{
			this.forcedAccountLogin = forcedAccountLogin;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteUTF(forcedAccountLogin);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			forcedAccountLogin = reader.ReadUTF();
		}
	}
}
