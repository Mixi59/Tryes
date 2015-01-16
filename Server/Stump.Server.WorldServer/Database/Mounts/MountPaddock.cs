﻿using Stump.ORM;
using Stump.ORM.SubSonic.SQLGeneration.Schema;

namespace Stump.Server.WorldServer.Database.Mounts
{
    public class MountPaddockRelator
    {
        public static string FetchByPaddockId = "SELECT * FROM mounts_paddocks WHERE PaddockId={0}";

        public static string FetchByMountId = "SELECT * FROM mounts_paddocks WHERE MountId={0}";
    }

    [TableName("mounts_paddocks")]
    public class MountPaddock : IAutoGeneratedRecord
    {
        [Index]
        public int PaddockId
        {
            get;
            set;
        }

        [PrimaryKey("MountId", false)]
        public int MountId
        {
            get;
            set;
        }

        public int CharacterId
        {
            get;
            set;
        }

        public bool Stabled
        {
            get;
            set;
        }
    }
}
