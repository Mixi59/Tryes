using Stump.Core.Attributes;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Game.Actors.Interfaces;

namespace Stump.Server.WorldServer.Game.Actors.Stats
{
    public class StatsAP : StatsData
    {
        [Variable]
        public static int APLimit = 12;

        public StatsAP(IStatsOwner owner, int valueBase)
            : base(owner, PlayerFields.AP, valueBase)
        {
        }

        public StatsAP(IStatsOwner owner, int valueBase, bool limit)
            : base(owner, PlayerFields.AP, valueBase)
        {
            Limit = limit;
        }

        public bool Limit
        {
            get;
            set;
        }

        public short Used
        {
            get;
            set;
        }

        public override int Equiped
        {
            get
            {
                return base.Equiped;
            }
            set
            {
                base.Equiped = value;
                if (Limit && Total > APLimit)
                    base.Equiped = APLimit;
            }
        }

        public int TotalMax
        {
            get
            {
                return Base + Equiped + Given + Context;
            }
        }

        public override int Total
        {
            get
            {
                return Base + Equiped + Given + Context - Used;
            }
        }
    }
}