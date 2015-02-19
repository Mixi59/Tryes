

// Generated on 02/18/2015 11:06:00
using System;
using System.Collections.Generic;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;

namespace Stump.DofusProtocol.D2oClasses
{
    [D2OClass("Skill", "com.ankamagames.dofus.datacenter.jobs")]
    [Serializable]
    public class Skill : IDataObject, IIndexedData
    {
        public const String MODULE = "Skills";
        public int id;
        [I18NField]
        public uint nameId;
        public int parentJobId;
        public Boolean isForgemagus;
        public int modifiableItemType;
        public int gatheredRessourceItem;
        public List<int> craftableItemIds;
        public int interactiveId;
        public String useAnimation;
        public Boolean isRepair;
        public int cursor;
        public Boolean availableInHouse;
        public uint levelMin;
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
        public uint NameId
        {
            get { return this.nameId; }
            set { this.nameId = value; }
        }
        [D2OIgnore]
        public int ParentJobId
        {
            get { return this.parentJobId; }
            set { this.parentJobId = value; }
        }
        [D2OIgnore]
        public Boolean IsForgemagus
        {
            get { return this.isForgemagus; }
            set { this.isForgemagus = value; }
        }
        [D2OIgnore]
        public int ModifiableItemType
        {
            get { return this.modifiableItemType; }
            set { this.modifiableItemType = value; }
        }
        [D2OIgnore]
        public int GatheredRessourceItem
        {
            get { return this.gatheredRessourceItem; }
            set { this.gatheredRessourceItem = value; }
        }
        [D2OIgnore]
        public List<int> CraftableItemIds
        {
            get { return this.craftableItemIds; }
            set { this.craftableItemIds = value; }
        }
        [D2OIgnore]
        public int InteractiveId
        {
            get { return this.interactiveId; }
            set { this.interactiveId = value; }
        }
        [D2OIgnore]
        public String UseAnimation
        {
            get { return this.useAnimation; }
            set { this.useAnimation = value; }
        }
        [D2OIgnore]
        public Boolean IsRepair
        {
            get { return this.isRepair; }
            set { this.isRepair = value; }
        }
        [D2OIgnore]
        public int Cursor
        {
            get { return this.cursor; }
            set { this.cursor = value; }
        }
        [D2OIgnore]
        public Boolean AvailableInHouse
        {
            get { return this.availableInHouse; }
            set { this.availableInHouse = value; }
        }
        [D2OIgnore]
        public uint LevelMin
        {
            get { return this.levelMin; }
            set { this.levelMin = value; }
        }
    }
}