﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using NLog;
using Stump.Core.Attributes;
using Stump.Core.Extensions;
using Stump.Core.IO;
using Stump.Core.Xml;
using Stump.Server.BaseServer.Initialization;
using Stump.Server.WorldServer.Database.World.Maps;
using Stump.Server.WorldServer.Game;
using Stump.Server.WorldServer.Game.Maps.Cells;
using Stump.Server.WorldServer.Game.Maps.Cells.Shapes;

namespace Stump.Plugins.DefaultPlugin.Global.Placements
{
    public static class PlacementsFix
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        [Variable]
        public static bool ActiveFix = false;

        [Variable]
        public static string SqlPatchName = "maps_placements_fix.sql";

        [Variable]
        public static string PatternsDir = "patterns";

        [Variable]
        public static byte SearchDeep = 5;

        [Initialization(typeof(Server.WorldServer.Game.World), Silent = true)]
        public static void ApplyFix()
        {
            if (!ActiveFix)
                return;

            var dir = Path.Combine(Path.GetDirectoryName(Plugin.CurrentPlugin.Context.AssemblyPath), PatternsDir);
            var patterns = new List<PlacementPattern>();
            var patternsNames = new Dictionary<PlacementPattern, string>();
            foreach (var file in Directory.EnumerateFiles(dir, "*.xml", SearchOption.AllDirectories))
            {
                try
                {
                    var pattern = XmlUtils.Deserialize<PlacementPattern>(file);

                    patterns.Add(pattern);
                    patternsNames.Add(pattern, Path.GetFileNameWithoutExtension(file));
                }
                catch (Exception)
                {
                    logger.Error("Cannot parse pattern {0}", file);
                }

            }

            var patchPath = Path.Combine(Path.GetDirectoryName(Plugin.CurrentPlugin.Context.AssemblyPath), SqlPatchName);

            if (File.Exists(patchPath))
                File.Delete(patchPath);

            var successCounter = new Dictionary<PlacementPattern, int>();
            var console = new ConsoleProgress();
            var maps = World.Instance.GetMaps().ToArray();
            int patches = 0;
            for (int i = 0; i < maps.Length; i++)
            {
                var map = maps[i];
                if (map.Record.BlueFightCells.Length == 0 ||
                    map.Record.RedFightCells.Length == 0)
                {
                    var fixPatterns = patterns.Where(entry => !entry.Relativ).Shuffle().ToArray();
                    PlacementPattern success = fixPatterns.FirstOrDefault(entry => entry.TestPattern(map));

                    if (success != null)
                    {
                        map.Record.BlueFightCells =
                            success.Blues.Select(entry => (short) MapPoint.CoordToCellId(entry.X, entry.Y)).ToArray();
                        map.Record.RedFightCells =
                            success.Reds.Select(entry => (short) MapPoint.CoordToCellId(entry.X, entry.Y)).ToArray();
                    }
                    else
                    {
                        var relativPatterns = patterns.Where(entry => entry.Relativ).Shuffle().ToArray();
                        var searchZone = new Lozenge(0, SearchDeep);

                        // 300 is approx. the middle of the map
                        foreach (var center in searchZone.GetCells(map.GetCell(300), map))
                        {
                            var centerPt = MapPoint.CellIdToCoord((uint) center.Id);
                            success = relativPatterns.FirstOrDefault(entry => entry.TestPattern(centerPt, map));

                            if (success != null)
                            {
                                map.Record.BlueFightCells =
                                    success.Blues.Select(
                                        entry =>
                                        (short) MapPoint.CoordToCellId(entry.X + centerPt.X, entry.Y + centerPt.Y)).
                                        ToArray();
                                map.Record.RedFightCells =
                                    success.Reds.Select(
                                        entry =>
                                        (short) MapPoint.CoordToCellId(entry.X + centerPt.X, entry.Y + centerPt.Y)).
                                        ToArray();
                                break;
                            }
                        }
                    }

                    // save it
                    if (success != null)
                    {
                        map.Record.Update();
                        map.UpdateFightPlacements();

                        var builder = new StringBuilder();
                        builder.Append("UPDATE `maps` SET BlueCells=0x");
                        builder.Append(string.Join("", MapRecord.SerializeFightCells(map.Record.BlueFightCells).Select(entry => entry.ToString("X2"))));
                        builder.Append(", RedCells=0x");
                        builder.Append(string.Join("", MapRecord.SerializeFightCells(map.Record.RedFightCells).Select(entry => entry.ToString("X2"))));
                        builder.Append(" WHERE Id='");
                        builder.Append(map.Id);
                        builder.Append("';");
                        File.AppendAllText(patchPath, builder + "\r\n");

                        patches++;
                        if (!successCounter.ContainsKey(success))
                            successCounter.Add(success, 1);
                        else successCounter[success]++;
                    }
                }


                console.Update(string.Format("{0:0.0}% ({1} patchs)", i / (double)maps.Length * 100, patches));
            }

            foreach (var counter in successCounter)
            {
                Console.WriteLine(patternsNames[counter.Key] + " : " + counter.Value);
            }

            logger.Debug("Maps placements fix applied");
        }
    }
}