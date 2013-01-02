using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Core.Reflection;
using Stump.Server.BaseServer.Database;
using Stump.Server.BaseServer.Initialization;
using Stump.Server.WorldServer.Database.World.Triggers;

namespace Stump.Server.WorldServer.Game.Maps.Cells.Triggers
{
    public class CellTriggerManager : DataManager<CellTriggerManager>
    {
        private Dictionary<int, CellTriggerRecord> m_cellTriggers;

        [Initialization(InitializationPass.Fourth)]
        public override void Initialize()
        {
            m_cellTriggers = Database.Query<CellTriggerRecord>(CellTriggerRecordRelator.FetchQuery).ToDictionary(entry => entry.Id);
        }

        public IEnumerable<CellTriggerRecord> GetCellTriggers()
        {
            return m_cellTriggers.Values;
        }

        public CellTriggerRecord GetOneCellTrigger(Predicate<CellTriggerRecord> predicate)
        {
            return m_cellTriggers.Values.FirstOrDefault(entry => predicate(entry));
        }

        public CellTriggerRecord GetCellTrigger(int id)
        {
            CellTriggerRecord cellTrigger;
            if (m_cellTriggers.TryGetValue(id, out cellTrigger))
                return cellTrigger;

            return cellTrigger;
        }

        public void AddCellTrigger(CellTriggerRecord cellTrigger)
        {
            Database.Insert(cellTrigger);
            m_cellTriggers.Add(cellTrigger.Id, cellTrigger);
        }
    }
}