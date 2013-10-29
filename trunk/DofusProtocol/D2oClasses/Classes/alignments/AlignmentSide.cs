

// Generated on 10/28/2013 14:03:17
using System;
using System.Collections.Generic;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;

namespace Stump.DofusProtocol.D2oClasses
{
    [D2OClass("AlignmentSide", "com.ankamagames.dofus.datacenter.alignments")]
    [Serializable]
    public class AlignmentSide : IDataObject, IIndexedData
    {
        private const String MODULE = "AlignmentSides";
        public int id;
        [I18NField]
        public uint nameId;
        public Boolean canConquest;
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
        public Boolean CanConquest
        {
            get { return canConquest; }
            set { canConquest = value; }
        }
    }
}