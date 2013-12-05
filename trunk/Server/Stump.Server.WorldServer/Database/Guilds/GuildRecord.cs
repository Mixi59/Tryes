﻿using System;
using Stump.ORM;
using Stump.ORM.SubSonic.SQLGeneration.Schema;

namespace Stump.Server.WorldServer.Database.Guilds
{
    public class GuildRelator
    {
        public static string FetchQuery = "SELECT * FROM guilds";
        /// <summary>
        /// Use string.Format
        /// </summary>
        public static string FetchById = "SELECT * FROM guilds WHERE Id={0}";
    }

    [TableName("guilds")]
    public class GuildRecord : IAutoGeneratedRecord
    {
        [PrimaryKey("Id", false)]
        public int Id
        {
            get;
            set;
        }

        [Ignore]
        public bool IsNew
        {
            get;
            set;
        }

        public DateTime CreationDate
        {
            get;
            set;
        }

        public String Name
        {
            get;
            set;
        }

        public long Experience
        {
            get;
            set;
        }

        public short EmblemBackgroundShape
        {
            get;
            set;
        }

        public int EmblemBackgroundColor
        {
            get;
            set;
        }

        public short EmblemForegroundShape
        {
            get;
            set;
        }

        public int EmblemForegroundColor
        {
            get;
            set;
        }
    }
}
