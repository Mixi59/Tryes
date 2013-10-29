

// Generated on 10/28/2013 14:03:21
using System;
using System.Collections.Generic;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;

namespace Stump.DofusProtocol.D2oClasses
{
    [D2OClass("Dungeon", "com.ankamagames.dofus.datacenter.world")]
    [Serializable]
    public class Dungeon : IDataObject, IIndexedData
    {
        private const String MODULE = "Dungeons";
        public int id;
        [I18NField]
        public uint nameId;
        public int optimalPlayerLevel;
        int IIndexedData.Id
        {
            get { return (int)id; }
        }
        [D2OIgnore]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        [D2OIgnore]
        public uint NameId
        {
            get { return nameId; }
            set { nameId = value; }
        }
        [D2OIgnore]
        public int OptimalPlayerLevel
        {
            get { return optimalPlayerLevel; }
            set { optimalPlayerLevel = value; }
        }
    }
}