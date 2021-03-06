

// Generated on 10/30/2016 16:20:56
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
    public class NamedPartyTeamWithOutcome
    {
        public const short Id = 470;
        public virtual short TypeId
        {
            get { return Id; }
        }
        
        public Types.NamedPartyTeam team;
        public short outcome;
        
        public NamedPartyTeamWithOutcome()
        {
        }
        
        public NamedPartyTeamWithOutcome(Types.NamedPartyTeam team, short outcome)
        {
            this.team = team;
            this.outcome = outcome;
        }
        
        public virtual void Serialize(IDataWriter writer)
        {
            team.Serialize(writer);
            writer.WriteVarShort(outcome);
        }
        
        public virtual void Deserialize(IDataReader reader)
        {
            team = new Types.NamedPartyTeam();
            team.Deserialize(reader);
            outcome = reader.ReadVarShort();
            if (outcome < 0)
                throw new Exception("Forbidden value on outcome = " + outcome + ", it doesn't respect the following condition : outcome < 0");
        }
        
        
    }
    
}