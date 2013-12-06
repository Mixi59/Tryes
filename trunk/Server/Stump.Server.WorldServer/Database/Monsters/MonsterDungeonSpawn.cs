﻿using System.Collections.Generic;
using Stump.DofusProtocol.Enums;
using Stump.ORM;
using Stump.ORM.SubSonic.SQLGeneration.Schema;
using Stump.Server.WorldServer.Game.Maps;
using Stump.Server.WorldServer.Game.Maps.Cells;

namespace Stump.Server.WorldServer.Database.Monsters
{
    public class MonsterDungeonSpawnRelator
    {
        public static string FetchQuery = "SELECT * FROM monsters_spawns_dungeons " +
                                          "LEFT JOIN monsters_spawns_dungeons_groups ON monsters_spawns_dungeons_groups.DungeonSpawnId = monsters_spawns_dungeons.Id " +
                                          "LEFT JOIN monsters_grades ON monsters_grades.Id = monsters_spawns_dungeons_groups.MonsterGradeId";

        private MonsterDungeonSpawn m_current;

        public MonsterDungeonSpawn Map(MonsterDungeonSpawn spawn, MonsterDungeonSpawnEntity dummy, MonsterGrade grade)
        {
            if (spawn == null)
                return m_current;

            if (m_current != null && m_current.Id == spawn.Id)
            {
                m_current.GroupMonsters.Add(grade);
                return null;
            }

            MonsterDungeonSpawn previous = m_current;

            m_current = spawn;
            m_current.GroupMonsters.Add(grade);

            return previous;
        }
    }


    /// <summary>
    /// Only used for many to many relation
    /// </summary>
    [TableName("monsters_spawns_dungeons_groups")]
    public class MonsterDungeonSpawnEntity : IAutoGeneratedRecord
    {
        [PrimaryKey("DungeonSpawnId", false)]
        public int DungeonSpawnId
        {
            get;
            set;
        }


        //[PrimaryKey("MonsterGradeId", false)]
        public int MonsterGradeId
        {
            get;
            set;
        }
    }

    [TableName("monsters_spawns_dungeons")]
    public class MonsterDungeonSpawn : IAutoGeneratedRecord
    {
        private List<MonsterGrade> m_groupMonsters = new List<MonsterGrade>();
        private Map m_map;
        private Map m_teleportMap;

        public int Id
        {
            get;
            set;
        }

        public int MapId
        {
            get;
            set;
        }

        [Ignore]
        public Map Map
        {
            get { return m_map ?? (m_map = Game.World.Instance.GetMap(MapId)); }
            set
            {
                m_map = value;
                MapId = value.Id;
            }
        }

        [Ignore]
        public List<MonsterGrade> GroupMonsters
        {
            get;
            set;
        }

        public bool TeleportEvent
        {
            get;
            set;
        }

        public int TeleportMapId
        {
            get;
            set;
        }

        [Ignore]
        public Map TeleportMap
        {
            get { return m_teleportMap ?? (m_teleportMap = Game.World.Instance.GetMap(TeleportMapId)); }
            set
            {
                m_teleportMap = value;
                TeleportMapId = value.Id;
            }
        }

        public short TeleportCell
        {
            get;
            set;
        }

        public DirectionsEnum TeleportDirection
        {
            get;
            set;
        }

        public ObjectPosition GetTeleportPosition()
        {
            return !TeleportEvent ? null : new ObjectPosition(TeleportMap, TeleportCell, TeleportDirection);
        }
    }
}