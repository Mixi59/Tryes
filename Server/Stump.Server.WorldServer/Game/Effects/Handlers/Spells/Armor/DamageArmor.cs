using System.Collections.Generic;
using System.Linq;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Fights.Buffs;
using Spell = Stump.Server.WorldServer.Game.Spells.Spell;

namespace Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Armor
{
    [EffectHandler(EffectsEnum.Effect_AddArmorDamageReduction)]
    [EffectHandler(EffectsEnum.Effect_AddGlobalDamageReduction_105)]
    public class DamageArmor : SpellEffectHandler
    {
        public DamageArmor(EffectDice effect, FightActor caster, Spell spell, Cell targetedCell, bool critical)
            : base(effect, caster, spell, targetedCell, critical)
        {
        }

        public override bool Apply()
        {
            foreach (var actor in GetAffectedActors())
            {
                var integerEffect = Effect.GenerateEffect(EffectGenerationContext.Spell) as EffectInteger;

                if (integerEffect == null)
                    return false;

                if (Effect.Duration <= 0)
                    return false;

                // these spells cannot stacks
                if (actor.GetBuffs(x => x.Effect.EffectId == Effect.EffectId && x.Spell.Template.Id == Spell.Template.Id).Any())
                    continue;

                AddTriggerBuff(actor, true, BuffTriggerType.BUFF_ADDED, ApplyArmorBuff, RemoveArmorBuff);
            }

            return true;
        }

        public static void ApplyArmorBuff(TriggerBuff buff, BuffTriggerType trigger, object token)
        {
            var integerEffect = buff.GenerateEffect();

            if (integerEffect == null)
                return;

            var target = buff.Target;
            if (target is SummonedBomb)
            {
                target = ((SummonedBomb) target).Summoner;
            }

            foreach (var caracteristic in GetAssociatedCaracteristics(buff.Spell.Id))
            {
                buff.Target.Stats[caracteristic].Context += target.CalculateArmorValue(integerEffect.Value);
            }
        }

        public static void RemoveArmorBuff(TriggerBuff buff)
        {
            var integerEffect = buff.GenerateEffect();

            if (integerEffect == null)
                return;

            foreach (var caracteristic in GetAssociatedCaracteristics(buff.Spell.Id))
            {
                buff.Target.Stats[caracteristic].Context -= buff.Target.CalculateArmorValue(integerEffect.Value);
            }
        }

        public static IEnumerable<PlayerFields> GetAssociatedCaracteristics(int spellId)
        {
            switch ((SpellIdEnum)spellId)
            {
                case SpellIdEnum.ARMURE_AQUEUSE:
                    yield return PlayerFields.WaterDamageArmor;
                    break;
                case SpellIdEnum.ARMURE_TERRESTRE:
                    yield return PlayerFields.EarthDamageArmor;
                    yield return PlayerFields.NeutralDamageArmor;
                    break;
                case SpellIdEnum.ARMURE_VENTEUSE:
                    yield return PlayerFields.AirDamageArmor;
                    break;
                case SpellIdEnum.ARMURE_INCANDESCENTE:
                    yield return PlayerFields.FireDamageArmor;
                    break;
                default:
                    yield return PlayerFields.GlobalDamageReduction;
                    break;
            }
        }
    }
}