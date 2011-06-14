// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'TaxCollectorInformationsInWaitForHelpState.xml' the '14/06/2011 11:32:49'
using System;
using Stump.BaseCore.Framework.IO;

namespace Stump.DofusProtocol.Types
{
	public class TaxCollectorInformationsInWaitForHelpState : TaxCollectorInformations
	{
		public const uint Id = 166;
		public short TypeId
		{
			get
			{
				return 166;
			}
		}
		
		public Types.ProtectedEntityWaitingForHelpInfo waitingForHelpInfo;
		
		public TaxCollectorInformationsInWaitForHelpState()
		{
		}
		
		public TaxCollectorInformationsInWaitForHelpState(int uniqueId, short firtNameId, short lastNameId, Types.AdditionalTaxCollectorInformations additonalInformation, short worldX, short worldY, short subAreaId, byte state, Types.EntityLook look, Types.ProtectedEntityWaitingForHelpInfo waitingForHelpInfo)
			 : base(uniqueId, firtNameId, lastNameId, additonalInformation, worldX, worldY, subAreaId, state, look)
		{
			this.waitingForHelpInfo = waitingForHelpInfo;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			waitingForHelpInfo.Serialize(writer);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			waitingForHelpInfo = new Types.ProtectedEntityWaitingForHelpInfo();
			waitingForHelpInfo.Deserialize(reader);
		}
	}
}
