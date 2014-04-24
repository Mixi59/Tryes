﻿using System.Linq;
using Stump.ORM.SubSonic.Query;
using Stump.Server.WorldServer.Game.Actors.RolePlay.TaxCollectors;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;

namespace Stump.Server.WorldServer.Game.Items.TaxCollector
{
    public class TaxCollectorBag : PersistantItemsCollection<TaxCollectorItem>
    {
        public TaxCollectorBag(TaxCollectorNpc owner)
        {
            Owner = owner;
        }

        public TaxCollectorNpc Owner
        {
            get;
            private set;
        }

        public int BagWeight
        {
            get { return (int) this.Sum(x => x.Template.RealWeight*x.Stack); }
        }

        public int BagValue
        {
            get { return (int) this.Sum(x => x.Template.Price*x.Stack); }
        }

        /// <summary>
        /// Must be saved 
        /// </summary>
        public bool IsDirty
        {
            get;
            private set;
        }

        protected override void OnItemStackChanged(TaxCollectorItem item, int difference)
        {
            IsDirty = true;

            base.OnItemStackChanged(item, difference);
        }

        protected override void OnItemAdded(TaxCollectorItem item)
        {
            IsDirty = true;

            base.OnItemAdded(item);
        }

        protected override void OnItemRemoved(TaxCollectorItem item)
        {
            IsDirty = true;

            if (Count == 0)
                Owner.Delete();

            base.OnItemRemoved(item);
        }

        public bool MoveToInventory(TaxCollectorItem item, Character character)
        {
            return MoveToInventory(item, character, item.Stack);
        }

        public bool MoveToInventory(TaxCollectorItem item, Character character, uint quantity)
        {
            if (quantity == 0)
                return false;

            if (quantity > item.Stack)
                quantity = item.Stack;

            RemoveItem(item, quantity);
            var newItem = ItemManager.Instance.CreatePlayerItem(character, item.Template, quantity,
                                                                       item.Effects);

            character.Inventory.AddItem(newItem);

            return true;
        }

        public void LoadRecord()
        {
            if (WorldServer.Instance.IsInitialized)
                WorldServer.Instance.IOTaskPool.EnsureContext();

            var records = ItemManager.Instance.FindTaxCollectorItems(Owner.Id);
            Items = records.Select(entry => new TaxCollectorItem(entry)).ToDictionary(entry => entry.Guid);
        }

        public void DeleteBag(bool lazySave = true)
        {
            DeleteAll(false);

            if (lazySave)
                WorldServer.Instance.IOTaskPool.AddMessage(() => Save());
            else
                Save();
        }

        public override void Save()
        {        
            if (WorldServer.Instance.IsInitialized)    
                WorldServer.Instance.IOTaskPool.EnsureContext();

            base.Save();

            IsDirty = false;
        }
    }
}
