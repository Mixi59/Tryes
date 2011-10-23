﻿using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Server.BaseServer.Database;
using Stump.Server.BaseServer.I18n;
using Stump.Server.WorldServer;
using Stump.Server.WorldServer.Database;
using Stump.Server.WorldServer.Database.I18n;
using Stump.Server.WorldServer.Database.Spells;
using Stump.Server.WorldServer.Worlds.Effects;
using Stump.Server.WorldServer.Worlds.Spells;

namespace Stump.Tools.SpellsExplorer
{
    internal static class Program
    {
        private static DatabaseAccessor m_databaseAccessor;

        private readonly static Languages SecondaryLanguage = Languages.French;

        public static void Main()
        {
            Console.BufferWidth = 90;
            Console.WindowWidth = 90;
            Console.WindowHeight = 45;
            
            Console.WriteLine("Initializing Database...");
            m_databaseAccessor = new DatabaseAccessor(WorldServer.DatabaseConfiguration, Definitions.DatabaseRevision, typeof (WorldBaseRecord<>), typeof (WorldBaseRecord<>).Assembly);
            m_databaseAccessor.Initialize();

            Console.WriteLine("Opening Database...");
            m_databaseAccessor.OpenDatabase();

            Console.WriteLine("Loading texts...");
            TextManager.Instance.Intialize();

            Console.WriteLine("Loading effects...");
            EffectManager.Instance.Initialize();

            Console.WriteLine("Loading spells...");
            SpellManager.Instance.Initialize();

            while (true)
            {
                Console.Write(">");
                string pattern = Console.ReadLine();
                try
                {
                    foreach (SpellTemplate spell in FindSpells(pattern))
                    {
                        ExploreSpell(spell, FindPatternSpellLevel(pattern));
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        public static IEnumerable<SpellTemplate> FindSpells(string pattern)
        {
            if (pattern.EndsWith("]") && pattern.LastIndexOf('[') != -1)
            {
                int selectorIndex = pattern.LastIndexOf('[');

                pattern = pattern.Remove(selectorIndex);
            }

            if (pattern.All(entry => entry >= '0' && entry <= '9'))
            {
                int id;

                if (!int.TryParse(pattern, out id))
                    yield break;

                yield return SpellManager.Instance.GetSpellTemplate(id);
            }
            else
            {
                foreach (SpellTemplate spell in SpellManager.Instance.GetSpellTemplates())
                {
                    if (string.IsNullOrEmpty(pattern) || spell.Name.Contains(pattern) || TextManager.Instance.GetText(spell.NameId, SecondaryLanguage).Contains(pattern))
                        yield return spell;
                }
            }
        }

        public static int FindPatternSpellLevel(string pattern)
        {
            if (pattern.EndsWith("]") && pattern.LastIndexOf('[') != -1)
            {
                int selectorIndex = pattern.LastIndexOf('[') + 1;

                return int.Parse(pattern.Substring(selectorIndex, pattern.LastIndexOf(']') - selectorIndex));
            }

            return 1;
        }

        public static void ExploreSpell(SpellTemplate spell, int level)
        {
            var levelTemplate = SpellManager.Instance.GetSpellLevel((int) spell.SpellLevelsIds[level - 1]);
            var type = SpellManager.Instance.GetSpellType(spell.TypeId);

            Console.WriteLine("Spell '{0}' : {1} - Level {2}", spell.Id, spell.Name, level);
            Console.WriteLine("Type : {0} - {1}", type.ShortName, type.LongName);
            Console.WriteLine("Level.SpellBreed = {0}, Level.HideEffects = {1}", levelTemplate.SpellBreed, levelTemplate.HideEffects);
            Console.WriteLine("");

            foreach (var effect in levelTemplate.Effects)
            {
                Console.WriteLine("Effect {0} ({1})", effect.EffectId, (int)effect.EffectId);
                Console.WriteLine("DiceFace = {0}, DiceNum = {1}, Value = {2}", effect.DiceFace, effect.DiceNum, effect.Value);
                Console.WriteLine("Hidden = {0}, Modificator = {1}, Random = {2}, Trigger = {3}", effect.Hidden, effect.Modificator, effect.Random, effect.Trigger);
                Console.WriteLine("ZoneShape = {0}, ZoneSize = {1}, Duration = {2}", effect.ZoneShape, effect.ZoneSize, effect.Duration);
                Console.WriteLine("Template.Active = {0}, Template.BonusType = {1}, Template.Boost = {2}", effect.Template.Active, effect.Template.BonusType, effect.Template.Boost);
                Console.WriteLine("Template.Category = {0}, Template.Characteristic = {1}, Template.ForceMinMax = {2}", effect.Template.Category, effect.Template.Characteristic, effect.Template.ForceMinMax);
                Console.WriteLine("Template.Operator = {0}, Template.Id = {1}, Template.ShowInSet = {2}", effect.Template.Operator, effect.Template.Id, effect.Template.ShowInSet);
                Console.WriteLine("Template.ShowInTooltip = {0}, Template.UseDice = {1}", effect.Template.ShowInTooltip, effect.Template.UseDice);
                Console.WriteLine("");
            }

            Console.WriteLine("");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("");
        }
    }
}