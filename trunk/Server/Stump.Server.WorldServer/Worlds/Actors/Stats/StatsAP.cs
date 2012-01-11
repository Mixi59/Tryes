using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Worlds.Actors.Interfaces;

namespace Stump.Server.WorldServer.Worlds.Actors.Stats
{
    public class StatsAP : StatsData
    {
        public StatsAP(IStatsOwner owner, short valueBase)
            : base(owner, CaracteristicsEnum.AP, valueBase)
        {
        }

        public short Used
        {
            get;
            set;
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