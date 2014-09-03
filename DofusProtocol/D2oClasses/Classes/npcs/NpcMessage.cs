

// Generated on 09/02/2014 22:34:38
using System;
using System.Collections.Generic;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;

namespace Stump.DofusProtocol.D2oClasses
{
    [D2OClass("NpcMessage", "com.ankamagames.dofus.datacenter.npcs")]
    [Serializable]
    public class NpcMessage : IDataObject, IIndexedData
    {
        public const String MODULE = "NpcMessages";
        public int id;
        public uint messageId;
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
        public uint MessageId
        {
            get { return this.messageId; }
            set { this.messageId = value; }
        }
    }
}