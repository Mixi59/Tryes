 


// Generated on 10/06/2013 14:22:01
using System;
using System.Collections.Generic;
using Stump.Core.IO;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;
using Stump.ORM;
using Stump.ORM.SubSonic.SQLGeneration.Schema;

namespace DBSynchroniser.Records
{
    [TableName("MonsterMiniBoss")]
    [D2OClass("MonsterMiniBoss")]
    public class MonsterMiniBossRecord : ID2ORecord
    {
        private const String MODULE = "MonsterMiniBoss";
        public int id;
        public int monsterReplacingId;

        [PrimaryKey("Id", false)]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int MonsterReplacingId
        {
            get { return monsterReplacingId; }
            set { monsterReplacingId = value; }
        }

        public virtual void AssignFields(object obj)
        {
            var castedObj = (MonsterMiniBoss)obj;
            
            Id = castedObj.id;
            MonsterReplacingId = castedObj.monsterReplacingId;
        }
        
        public virtual object CreateObject()
        {
            
            var obj = new MonsterMiniBoss();
            obj.id = Id;
            obj.monsterReplacingId = MonsterReplacingId;
            return obj;
        
        }
    }
}