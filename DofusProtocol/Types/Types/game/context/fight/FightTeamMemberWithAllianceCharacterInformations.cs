

// Generated on 04/24/2015 03:38:21
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
    public class FightTeamMemberWithAllianceCharacterInformations : FightTeamMemberCharacterInformations
    {
        public const short Id = 426;
        public override short TypeId
        {
            get { return Id; }
        }
        
        public Types.BasicAllianceInformations allianceInfos;
        
        public FightTeamMemberWithAllianceCharacterInformations()
        {
        }
        
        public FightTeamMemberWithAllianceCharacterInformations(int id, string name, byte level, Types.BasicAllianceInformations allianceInfos)
         : base(id, name, level)
        {
            this.allianceInfos = allianceInfos;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            allianceInfos.Serialize(writer);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            allianceInfos = new Types.BasicAllianceInformations();
            allianceInfos.Deserialize(reader);
        }
        
        
    }
    
}