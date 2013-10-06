

// Generated on 10/06/2013 17:58:56
using System;
using System.Collections.Generic;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;

namespace Stump.DofusProtocol.D2oClasses
{
    [D2OClass("SoundBones", "com.ankamagames.dofus.datacenter.sounds")]
    [Serializable]
    public class SoundBones : IDataObject, IIndexedData
    {
        public uint id;
        public List<String> keys;
        public List<List<SoundAnimation>> values;
        public String MODULE = "SoundBones";
        int IIndexedData.Id
        {
            get { return (int)id; }
        }
        [D2OIgnore]
        public uint Id
        {
            get { return id; }
            set { id = value; }
        }
        [D2OIgnore]
        public List<String> Keys
        {
            get { return keys; }
            set { keys = value; }
        }
        [D2OIgnore]
        public List<List<SoundAnimation>> Values
        {
            get { return values; }
            set { values = value; }
        }
    }
}