 


// Generated on 10/06/2013 14:21:58
using System;
using System.Collections.Generic;
using Stump.Core.IO;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;
using Stump.ORM;
using Stump.ORM.SubSonic.SQLGeneration.Schema;

namespace DBSynchroniser.Records
{
    [TableName("Appearances")]
    [D2OClass("Appearance")]
    public class AppearanceRecord : ID2ORecord
    {
        public const String MODULE = "Appearances";
        public uint id;
        public uint type;
        public String data;

        [PrimaryKey("Id", false)]
        public uint Id
        {
            get { return id; }
            set { id = value; }
        }

        public uint Type
        {
            get { return type; }
            set { type = value; }
        }

        [NullString]
        public String Data
        {
            get { return data; }
            set { data = value; }
        }

        public virtual void AssignFields(object obj)
        {
            var castedObj = (Appearance)obj;
            
            Id = castedObj.id;
            Type = castedObj.type;
            Data = castedObj.data;
        }
        
        public virtual object CreateObject()
        {
            
            var obj = new Appearance();
            obj.id = Id;
            obj.type = Type;
            obj.data = Data;
            return obj;
        
        }
    }
}