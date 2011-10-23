using System;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Worlds.Actors.Fight;
using Stump.Server.WorldServer.Worlds.Effects.Instances;
using Stump.Server.WorldServer.Worlds.Spells;

namespace Stump.Server.WorldServer.Worlds.Effects.Handlers.Spells.Debuffs
{
    [EffectHandler(EffectsEnum.Effect_RemoveAP)]
    public class RemoveAP : SpellEffectHandler
    {
        public RemoveAP(EffectDice effect, FightActor caster, Spell spell, Cell targetedCell, bool critical)
            : base(effect, caster, spell, targetedCell, critical)
        {
        }

        public override void Apply()
        {
            var integerEffect = Effect.GenerateEffect(EffectGenerationContext.Spell) as EffectInteger;

            if (integerEffect == null)
                return;

            foreach (FightActor actor in GetAffectedActors())
            {
                if (Effect.Duration > 1)
                {
                    AddStatBuff(actor, (short)( -integerEffect.Value ), CaracteristicsEnum.AP, true);
                }
                else
                {
                    actor.LostAP(integerEffect.Value);
                }
            }
        }
    }
}