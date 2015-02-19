

// Generated on 02/18/2015 11:06:00
using System;
using System.Collections.Generic;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;

namespace Stump.DofusProtocol.D2oClasses
{
    [D2OClass("CompanionCharacteristic", "com.ankamagames.dofus.datacenter.monsters")]
    [Serializable]
    public class CompanionCharacteristic : IDataObject, IIndexedData
    {
        public const String MODULE = "CompanionCharacteristics";
        public int id;
        public int caracId;
        public int companionId;
        public int order;
        public int initialValue;
        public int levelPerValue;
        public int valuePerLevel;
        int IIndexedData.Id
        {
            get { return (int)id; }
        }
        [D2OIgnore]
        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
        [D2OIgnore]
        public int CaracId
        {
            get { return this.caracId; }
            set { this.caracId = value; }
        }
        [D2OIgnore]
        public int CompanionId
        {
            get { return this.companionId; }
            set { this.companionId = value; }
        }
        [D2OIgnore]
        public int Order
        {
            get { return this.order; }
            set { this.order = value; }
        }
        [D2OIgnore]
        public int InitialValue
        {
            get { return this.initialValue; }
            set { this.initialValue = value; }
        }
        [D2OIgnore]
        public int LevelPerValue
        {
            get { return this.levelPerValue; }
            set { this.levelPerValue = value; }
        }
        [D2OIgnore]
        public int ValuePerLevel
        {
            get { return this.valuePerLevel; }
            set { this.valuePerLevel = value; }
        }
    }
}