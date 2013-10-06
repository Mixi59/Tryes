

// Generated on 10/06/2013 17:58:52
using System;
using System.Collections.Generic;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;

namespace Stump.DofusProtocol.D2oClasses
{
    [D2OClass("AlignmentOrder", "com.ankamagames.dofus.datacenter.alignments")]
    [Serializable]
    public class AlignmentOrder : IDataObject, IIndexedData
    {
        private const String MODULE = "AlignmentOrder";
        public int id;
        public uint nameId;
        public uint sideId;
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
        public uint SideId
        {
            get { return sideId; }
            set { sideId = value; }
        }
    }
}