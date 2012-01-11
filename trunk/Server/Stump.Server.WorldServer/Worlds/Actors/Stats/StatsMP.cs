using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Worlds.Actors.Interfaces;

namespace Stump.Server.WorldServer.Worlds.Actors.Stats
{
    public class StatsMP : StatsData
    {
        public StatsMP(IStatsOwner owner, short valueBase)
            : base(owner, CaracteristicsEnum.MP, valueBase)
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