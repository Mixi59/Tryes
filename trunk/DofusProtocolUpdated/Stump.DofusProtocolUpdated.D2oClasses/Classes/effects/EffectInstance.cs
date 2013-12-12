

// Generated on 12/12/2013 16:57:37
using System;
using System.Collections.Generic;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;

namespace Stump.DofusProtocol.D2oClasses
{
    [D2OClass("EffectInstance", "com.ankamagames.dofus.datacenter.effects")]
    [Serializable]
    public class EffectInstance : IDataObject, IIndexedData
    {
        public uint effectId;
        public int targetId;
        public String targetMask;
        public int duration;
        public int delay;
        public int random;
        public int group;
        public int modificator;
        public Boolean trigger;
        public Boolean hidden;
        public uint zoneSize;
        public uint zoneShape;
        public uint zoneMinSize;
        public String rawZone;
        int IIndexedData.Id
        {
            get { return (int)effectId; }
        }
        [D2OIgnore]
        public uint EffectId
        {
            get { return effectId; }
            set { effectId = value; }
        }
        [D2OIgnore]
        public int TargetId
        {
            get { return targetId; }
            set { targetId = value; }
        }
        [D2OIgnore]
        public String TargetMask
        {
            get { return targetMask; }
            set { targetMask = value; }
        }
        [D2OIgnore]
        public int Duration
        {
            get { return duration; }
            set { duration = value; }
        }
        [D2OIgnore]
        public int Delay
        {
            get { return delay; }
            set { delay = value; }
        }
        [D2OIgnore]
        public int Random
        {
            get { return random; }
            set { random = value; }
        }
        [D2OIgnore]
        public int Group
        {
            get { return group; }
            set { group = value; }
        }
        [D2OIgnore]
        public int Modificator
        {
            get { return modificator; }
            set { modificator = value; }
        }
        [D2OIgnore]
        public Boolean Trigger
        {
            get { return trigger; }
            set { trigger = value; }
        }
        [D2OIgnore]
        public Boolean Hidden
        {
            get { return hidden; }
            set { hidden = value; }
        }
        [D2OIgnore]
        public uint ZoneSize
        {
            get { return zoneSize; }
            set { zoneSize = value; }
        }
        [D2OIgnore]
        public uint ZoneShape
        {
            get { return zoneShape; }
            set { zoneShape = value; }
        }
        [D2OIgnore]
        public uint ZoneMinSize
        {
            get { return zoneMinSize; }
            set { zoneMinSize = value; }
        }
        [D2OIgnore]
        public String RawZone
        {
            get { return rawZone; }
            set { rawZone = value; }
        }
    }
}