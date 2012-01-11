// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'IdentificationSuccessMessage.xml' the '09/12/2011 21:48:24'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class IdentificationSuccessMessage : Message
	{
		public const uint Id = 22;
		public override uint MessageId
		{
			get
			{
				return 22;
			}
		}
		
		public bool hasRights;
		public bool wasAlreadyConnected;
		public string login;
		public string nickname;
		public int accountId;
		public sbyte communityId;
		public string secretQuestion;
		public double subscriptionEndDate;
		
		public IdentificationSuccessMessage()
		{
		}
		
		public IdentificationSuccessMessage(bool hasRights, bool wasAlreadyConnected, string login, string nickname, int accountId, sbyte communityId, string secretQuestion, double subscriptionEndDate)
		{
			this.hasRights = hasRights;
			this.wasAlreadyConnected = wasAlreadyConnected;
			this.login = login;
			this.nickname = nickname;
			this.accountId = accountId;
			this.communityId = communityId;
			this.secretQuestion = secretQuestion;
			this.subscriptionEndDate = subscriptionEndDate;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			byte flag1 = 0;
			flag1 = BooleanByteWrapper.SetFlag(flag1, 0, hasRights);
			flag1 = BooleanByteWrapper.SetFlag(flag1, 1, wasAlreadyConnected);
			writer.WriteByte(flag1);
			writer.WriteUTF(login);
			writer.WriteUTF(nickname);
			writer.WriteInt(accountId);
			writer.WriteSByte(communityId);
			writer.WriteUTF(secretQuestion);
			writer.WriteDouble(subscriptionEndDate);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			byte flag1 = reader.ReadByte();
			hasRights = BooleanByteWrapper.GetFlag(flag1, 0);
			wasAlreadyConnected = BooleanByteWrapper.GetFlag(flag1, 1);
			login = reader.ReadUTF();
			nickname = reader.ReadUTF();
			accountId = reader.ReadInt();
			if ( accountId < 0 )
			{
				throw new Exception("Forbidden value on accountId = " + accountId + ", it doesn't respect the following condition : accountId < 0");
			}
			communityId = reader.ReadSByte();
			if ( communityId < 0 )
			{
				throw new Exception("Forbidden value on communityId = " + communityId + ", it doesn't respect the following condition : communityId < 0");
			}
			secretQuestion = reader.ReadUTF();
			subscriptionEndDate = reader.ReadDouble();
			if ( subscriptionEndDate < 0 )
			{
				throw new Exception("Forbidden value on subscriptionEndDate = " + subscriptionEndDate + ", it doesn't respect the following condition : subscriptionEndDate < 0");
			}
		}
	}
}
