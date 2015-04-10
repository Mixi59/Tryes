﻿using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Enums.Custom;
using Stump.Server.WorldServer.Game.Actors.Fight;

namespace Stump.Server.WorldServer.Game.Fights.Challenges.Custom
{
    [ChallengeIdentifier((int)ChallengeEnum.CIRCULEZ)]
    public class TimeFliesChallenge : DefaultChallenge
    {
        public TimeFliesChallenge(IFight fight)
            : base(fight)
        {
        }

        public TimeFliesChallenge(int id, IFight fight)
            : base(id, fight)
        {
            Bonus = 30;

            foreach (var fighter in fight.GetAllFighters<MonsterFighter>())
            {
                fighter.FightPointsVariation += OnFightPointsVariation;
            }
        }

        private void OnFightPointsVariation(FightActor actor, ActionsEnum action, FightActor source, FightActor target, short delta)
        {
            if (delta >= 0)
                return;

            if (actor.IsFriendlyWith(source))
                return;

            if (action != ActionsEnum.ACTION_CHARACTER_ACTION_POINTS_LOST)
                return;

            UpdateStatus(ChallengeStatusEnum.FAILED, source);
        }
    }
}
