

// Generated on 12/12/2013 16:57:41
using System;
using System.Collections.Generic;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;

namespace Stump.DofusProtocol.D2oClasses
{
    [D2OClass("OptionalFeature", "com.ankamagames.dofus.datacenter.misc")]
    [Serializable]
    public class OptionalFeature : IDataObject, IIndexedData
    {
        public const String MODULE = "OptionalFeatures";
        public int id;
        public String keyword;
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
        public String Keyword
        {
            get { return keyword; }
            set { keyword = value; }
        }
    }
}