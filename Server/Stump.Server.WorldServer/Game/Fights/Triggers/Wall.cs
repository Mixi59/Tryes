﻿using System.Linq;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Types;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Damage;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Spells;

namespace Stump.Server.WorldServer.Game.Fights.Triggers
{
    public class Wall : MarkTrigger
    {
        public Wall(short id, FightActor caster, Spell castedSpell, EffectDice originEffect, Cell centerCell, WallsBinding binding, MarkShape shape)
            : base(id, caster, castedSpell, originEffect, centerCell, shape)
        {
            WallBinding = binding;
        }

        public WallsBinding WallBinding
        {
            get;
            private set;
        }

        public override GameActionMarkTypeEnum Type
        {
            get { return GameActionMarkTypeEnum.WALL; }
        }

        public override TriggerType TriggerType
        {
            get { return TriggerType.MOVE | TriggerType.OnTurnBegin | TriggerType.OnTurnEnd; }
        }

        public SummonedBomb[] Bombs
        {
            get { return new[] { WallBinding.Bomb1, WallBinding.Bomb2 }; }
        }

        public override void Trigger(FightActor trigger)
        {
            if (!CanTrigger(trigger))
                return;

            NotifyTriggered(trigger, CastedSpell);

            var handler = SpellManager.Instance.GetSpellCastHandler(Caster, CastedSpell, trigger.Cell, false);
            handler.MarkTrigger = this;
            handler.Initialize();
            var bonus = Bombs.Sum(x => x.DamageBonusPercent);

            foreach (var effect in handler.GetEffectHandlers().OfType<DirectDamage>())
            {
                effect.Efficiency = 1 + bonus/100d;
            }

            handler.Execute();
        }

        public override GameActionMark GetGameActionMark()
        {
            return new GameActionMark(Caster.Id, (sbyte)Caster.Team.Id, CastedSpell.Template.Id, (sbyte)CastedSpell.CurrentLevel, (short)CastedSpell.Id,
                (sbyte)Type, CenterCell.Id, Shape.GetGameActionMarkedCells(), true);
        }

        public override GameActionMark GetHiddenGameActionMark()
        {
            return GetGameActionMark();
        }

        public override bool DoesSeeTrigger(FightActor fighter)
        {
            return true;
        }

        public override bool DecrementDuration()
        {
            return false;
        }

        public override bool CanTrigger(FightActor actor)
        {
            var bomb = Bombs.FirstOrDefault();

            if (bomb == null)
                return true;

            if ((actor is SummonedBomb))
            {
                var triggerBomb = ((SummonedBomb)actor);

                if (bomb.IsFriendlyWith(triggerBomb))
                    return false;

                if (bomb.MonsterBombTemplate == triggerBomb.MonsterBombTemplate)
                    return false;
            }
            else if (actor.HasState((int)SpellStatesEnum.KABOOM_92))
            {
                if (bomb.IsFriendlyWith(actor))
                    return false;
            }

            return true;
        }
    }
}