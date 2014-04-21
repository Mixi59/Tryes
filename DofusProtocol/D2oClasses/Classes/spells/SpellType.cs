

// Generated on 10/28/2013 14:03:21
using System;
using System.Collections.Generic;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;

namespace Stump.DofusProtocol.D2oClasses
{
    [D2OClass("SpellType", "com.ankamagames.dofus.datacenter.spells")]
    [Serializable]
    public class SpellType : IDataObject, IIndexedData
    {
        private const String MODULE = "SpellTypes";
        public int id;
        [I18NField]
        public uint longNameId;
        [I18NField]
        public uint shortNameId;
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
        public uint LongNameId
        {
            get { return longNameId; }
            set { longNameId = value; }
        }
        [D2OIgnore]
        public uint ShortNameId
        {
            get { return shortNameId; }
            set { shortNameId = value; }
        }
    }
}