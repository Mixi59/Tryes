﻿using System;
using System.Collections.Generic;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.Characters;
using Stump.Server.WorldServer.Handlers.Inventory;
using Stump.Server.WorldServer.Worlds.Actors.RolePlay.Characters;

namespace Stump.Server.WorldServer.Worlds.Spells
{
    public class SpellInventory
    {
        private readonly Dictionary<int, Spell> m_spells = new Dictionary<int, Spell>();
        private readonly object m_locker = new object();

        public SpellInventory(Character owner)
        {
            Owner = owner;

            LoadSpells();
        }

        public Character Owner
        {
            get;
            private set;
        }

        private void LoadSpells()
        {
            foreach (var record in Owner.Record.Spells)
            {
                var spell = new Spell(record);

                m_spells.Add(spell.Id, spell);
            }
        }

        public Spell GetSpell(int id)
        {
            Spell spell;
            if (m_spells.TryGetValue(id, out spell))
                return spell;

            return null;
        }

        public bool HasSpell(int id)
        {
            return m_spells.ContainsKey(id);
        }

        public bool HasSpell(Spell spell)
        {
            return m_spells.ContainsKey(spell.Id);
        }

        public IEnumerable<Spell> GetSpells()
        {
            return m_spells.Values; 
        }

        public Spell LearnSpell(int id)
        {
            var template = SpellManager.Instance.GetSpellTemplate(id);

            if (template == null)
                return null;

            var record = SpellManager.Instance.CreateSpellRecord(Owner.Record, template);
            Owner.Record.Spells.Add(record);

            var spell = new Spell(record);

            lock (m_locker)
                m_spells.Add(spell.Id, spell);

            return spell;
        }

        public void UnLearnSpell(int id)
        {
            var spell = GetSpell(id);

            if (spell == null)
                return;

            lock (m_locker)
                m_spells.Remove(id);

            Owner.Record.Spells.Remove(spell.Record);
        }

        public bool BoostSpell(int id)
        {
            var spell = GetSpell(id);

            if (spell == null)
            {
                InventoryHandler.SendSpellUpgradeFailureMessage(Owner.Client);
                return false;
            }

            if (Owner.SpellsPoints < spell.CurrentLevel)
            {
                InventoryHandler.SendSpellUpgradeFailureMessage(Owner.Client);
                return false;
            }

            Owner.SpellsPoints -= (ushort)spell.CurrentLevel;
            spell.CurrentLevel++;

            InventoryHandler.SendSpellUpgradeSuccessMessage(Owner.Client, spell);

            return true;
        }

        public void MoveSpell(int id, byte position)
        {
            var spell = GetSpell(id);

            if (spell == null)
                return;

            // todo move to shortcut bar
        }
    }
}