﻿using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Effects.Instances;using Stump.Server.WorldServer.Game.Spells.Casts;

namespace Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Heal
{
    [EffectHandler(EffectsEnum.Effect_GiveHPPercent)]
    public class GiveHpPercent : SpellEffectHandler
    {
        public GiveHpPercent(EffectDice effect, FightActor caster, SpellCastHandler castHandler, Cell targetedCell, bool critical)
            : base(effect, caster, castHandler, targetedCell, critical)
        {
        }

        protected override bool InternalApply()
        {
            var integerEffect = GenerateEffect();

            if (integerEffect == null)
                return false;

            var damageAmount = 0;
            foreach (var actor in GetAffectedActors())
            {
                HealHpPercent(actor, integerEffect.Value);

                if (Effect.Duration == 0)
                    damageAmount = DealHpPercent(integerEffect.Value);
            }

            if (damageAmount > 0)
                Caster.InflictDirectDamage(damageAmount, Caster);

            return true;
        }

        // Todo: reduce duplication (see RestoreHpPercent)
        void HealHpPercent(FightActor actor, int percent)
        {
            var healAmount = (int)(Caster.LifePoints * (percent / 100d));

            var damage = new Fights.Damage(Dice, EffectSchoolEnum.Healing, Caster, Spell, TargetedCell, EffectZone)
            {
                MarkTrigger = MarkTrigger,
                IsCritical = Critical
            };
            damage.GenerateDamages();
            damage.Amount = healAmount;

            actor.Heal(healAmount, Caster, true);
        }

        int DealHpPercent(int percent) => (int)(Caster.LifePoints * (percent / 100.0));
    }
}