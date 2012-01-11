﻿using NLog;
using Stump.Server.BaseServer.Initialization;

namespace Stump.Plugins.DefaultPlugin.World
{
    public static class IncarnamSpawnsFix
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private const int INCARNAM_SUPERAREA_ID = 3;

        [Initialization(typeof(Server.WorldServer.Worlds.World), Silent = true)]
        public static void ApplyFix()
        {
            logger.Debug("Apply incarnam spawns fix");

            var area = Server.WorldServer.Worlds.World.Instance.GetSuperArea(INCARNAM_SUPERAREA_ID);

            if (area == null)
            {
                logger.Debug("Fix not applied");
                return;
            }

            foreach (var map in area.Maps)
            {
                // disable it first to avoid conflicts
                map.DisableMonsterSpawns();

                map.EnableMonsterSpawns(new IncarnamSpawningPool(map, map.SubArea.GetMonsterSpawnInterval()));
            }
        }
    }
}