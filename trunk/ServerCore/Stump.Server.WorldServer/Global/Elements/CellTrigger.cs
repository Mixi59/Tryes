using System;
using System.Xml.Serialization;
using Stump.Server.WorldServer.Actions;
using Stump.Server.WorldServer.Entities;
using Stump.Server.WorldServer.XmlSerialize;

namespace Stump.Server.WorldServer.Global.Maps
{
    public class CellTrigger : Localizable
    {
        [Flags]
        public enum TriggerEvent
        {
            OnReached = 1,
        }

        private CellTrigger()
        {
        }

        public CellTrigger(ushort cellId, uint mapId, TriggerEvent @event, ActionSerialized action)
            : base(cellId, mapId)
        {
            Event = @event;
            Action = action;
        }

        public TriggerEvent Event
        {
            get;
            set;
        }

        public ActionSerialized Action
        {
            get;
            set;
        }

        public void StartTrigger()
        {
            if (Event.HasFlag(TriggerEvent.OnReached))
                Cell.CellReached += CellReached;
        }

        private void CellReached(CellLinked cell, Character character)
        {
            ActionBase.ExecuteAction(Action, cell, character);
        }

        public void StopTrigger()
        {
            if (Event.HasFlag(TriggerEvent.OnReached))
                Cell.CellReached -= CellReached;
        }
    }
}