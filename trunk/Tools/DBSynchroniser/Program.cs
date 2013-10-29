﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using DBSynchroniser.Records;
using DBSynchroniser.Records.Icons;
using DBSynchroniser.Records.Langs;
using Stump.Core.Attributes;
using Stump.Core.Reflection;
using Stump.Core.Sql;
using Stump.Core.Xml.Config;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.D2oClasses.Tools.D2i;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;
using Stump.DofusProtocol.D2oClasses.Tools.D2p;
using Stump.ORM;
using Stump.ORM.SubSonic.SQLGeneration.Schema;
using Stump.Server.WorldServer;
using Stump.Server.WorldServer.Database;
using Stump.Server.WorldServer.Game;

namespace DBSynchroniser
{
    internal class Program
    {
        public const string ConfigFile = "config.xml";
        private static XmlConfig m_config;

        public static DatabaseAccessor Database;

        [Variable] public static readonly DatabaseConfiguration DatabaseConfiguration = new DatabaseConfiguration
        {
            DbName = "stump_data",
            Host = "localhost",
            User = "root",
            Password = "",
            ProviderName = "MySql.Data.MySqlClient",
        };

        [Variable] public static readonly DatabaseConfiguration WorldDatabaseConfiguration = new DatabaseConfiguration
        {
            DbName = "stump_world",
            Host = "localhost",
            User = "root",
            Password = "",
            ProviderName = "MySql.Data.MySqlClient",
        };

        [Variable(true)] public static string SpecificLanguage = "fr,en";

        [Variable(true)] public static string DofusCustomPath = "";

        [Variable(true)] public static string FilesOutput = "./generate";


        private static readonly Tuple<string, Action>[] m_menus =
        {
            Tuple.Create<string, Action>("Set Dofus Path (empty = default)", SetDofusPath),
            Tuple.Create<string, Action>("Set languages (empty = all)", SetLanguages),
            Tuple.Create<string, Action>("Create database", CreateDatabase),
            Tuple.Create<string, Action>("Load langs", LoadLangsWithWarning),
            Tuple.Create<string, Action>("Load icons files", LoadIconsWithWarnings),
            Tuple.Create<string, Action>("Generate client files", GenerateFiles),
            Tuple.Create<string, Action>("Synchronise world database", SyncDatabases)
        };

        private static Dictionary<string, D2OTable> m_tables = new Dictionary<string, D2OTable>();

        private static void Main()
        {
            m_tables = EnumerateTables(Assembly.GetExecutingAssembly()).ToDictionary(x => x.ClassName);
            Console.WriteLine("Load {0}...", ConfigFile);
            m_config = new XmlConfig(ConfigFile);
            m_config.AddAssembly(Assembly.GetExecutingAssembly());

            if (!File.Exists(ConfigFile))
                m_config.Create();
            else
                m_config.Load();


            Console.WriteLine("Open database ...");
            Database = new DatabaseAccessor(DatabaseConfiguration);
            Database.RegisterMappingAssembly(Assembly.GetExecutingAssembly());
            Database.Initialize();

            try
            {
                Database.OpenConnection();
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot open database : {0}", e);
                Exit("Modify config file");
            }

            while (true)
            {
                ShowMenus();

                try
                {
                    string input = Console.ReadLine();
                    int number;
                    while (!int.TryParse(input, out number) || number < 1 || number > m_menus.Length)
                    {
                        Console.WriteLine("Give a valid number between 1 and {0}", m_menus.Length);
                        input = Console.ReadLine();
                    }

                    Console.Clear();
                    m_menus[number - 1].Item2();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error : " + ex);
                }

                Console.WriteLine("Press Enter to return to the menu");
                Console.ReadLine();
                Console.Clear();
            }
        }

        private static IEnumerable<D2OTable> EnumerateTables(Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes())
            {
                var attr = type.GetCustomAttribute<D2OClassAttribute>();
                if (attr != null)
                {
                    var tableAttr = type.GetCustomAttribute<TableNameAttribute>();

                    if (tableAttr != null)
                    {
                        var table = new D2OTable
                        {
                            Type = type,
                            ClassName = attr.Name,
                            TableName = tableAttr.TableName,
                            Constructor = type.GetConstructor(new Type[0]).CreateDelegate()
                        };

                        yield return table;
                    }
                }
            }

        }

        private static void ShowMenus()
        {
            Console.WriteLine("Dofus path = {0}", FindDofusPath());
            Console.WriteLine("Languages = {0}", SpecificLanguage);
            Console.WriteLine();
            Console.WriteLine("What to do ?");
            int number = 1;
            foreach (var menu in m_menus)
            {
                Console.WriteLine(number + ". " + menu.Item1);
                number++;
            }
        }

        private static string FindDofusPath()
        {
            if (!string.IsNullOrEmpty(DofusCustomPath))
                return DofusCustomPath;

            string programFiles = Environment.GetEnvironmentVariable("programfiles(x86)");

            if (string.IsNullOrEmpty(programFiles))
                programFiles = Environment.GetEnvironmentVariable("programfiles");

            if (string.IsNullOrEmpty(programFiles))
                return Path.Combine(AskDofusPath(), "app");

            if (Directory.Exists(Path.Combine(programFiles, "Dofus2", "app")))
                return Path.Combine(programFiles, "Dofus2", "app");
            if (Directory.Exists(Path.Combine(programFiles, "Dofus 2", "app")))
                return Path.Combine(programFiles, "Dofus 2", "app");

            string dofusDataPath = Path.Combine(AskDofusPath(), "app");

            if (!Directory.Exists(dofusDataPath))
                throw new Exception("Dofus data path not found");

            return dofusDataPath;
        }

        private static string AskDofusPath()
        {
            Console.WriteLine("Dofus path not found. Enter Dofus 2 root folder (%programFiles%/Dofus2):");

            return Path.GetFullPath(Console.ReadLine());
        }

        private static void Exit(string reason = "")
        {
            if (!string.IsNullOrEmpty(reason))
                Console.WriteLine(reason);

            Console.WriteLine("Press enter to exit");
            Console.Read();

            Environment.Exit(-1);
        }

        #region Menus actions

        private static void SetDofusPath()
        {
            Console.WriteLine("Enter dofus path (empty = default path):");
            string path = Console.ReadLine();

            if (!Directory.Exists(path))
                Console.WriteLine("Path '{0}' doesn't exists", path);
            else
            {
                DofusCustomPath = path;

                m_config.Save();

                Console.WriteLine("Path changed (saved)");
            }
        }

        private static void SetLanguages()
        {
            Console.WriteLine("Enter languages (default = fr,en) :");
            string langs = Console.ReadLine();
            SpecificLanguage = langs;

            m_config.Save();

            Console.WriteLine("Languages changed (saved)");
        }

        private static void CreateDatabase()
        {
            string d2oFolder = Path.Combine(FindDofusPath(), "data", "common");

            Console.WriteLine("WARNING IT WILL ERASE ALL TABLES. ARE YOU SURE ? (y/n)");
            if (Console.ReadLine() != "y")
                return;

            foreach (var table in m_tables)
            {
                Database.Database.Execute("DELETE FROM " + table.Value.TableName);
                Database.Database.Execute("ALTER TABLE " + table.Value.TableName + " AUTO_INCREMENT=1");
            }

            foreach (string filePath in Directory.EnumerateFiles(d2oFolder))
            {
                string ext = Path.GetExtension(filePath);
                if (ext != ".d2o" && ext != ".d2os")
                    continue;

                Console.Write("Import {0}...", Path.GetFileName(filePath));

                int cursorLeft = Console.CursorLeft;
                int cursorTop = Console.CursorTop;
                int i = 0;
                var d2oReader = new D2OReader(filePath);
                foreach (var entry in d2oReader.GetObjectsClasses())
                {
                    D2OTable table = !m_tables.ContainsKey(entry.Value.Name)
                        ? m_tables[entry.Value.ClassType.BaseType.Name]
                        : m_tables[entry.Value.Name];
                    object obj = d2oReader.ReadObject(entry.Key);

                    var record = table.Constructor.DynamicInvoke() as ID2ORecord;
                    record.AssignFields(obj);

                    Database.Database.Insert(record);
                    
                    i++;
                    Console.SetCursorPosition(cursorLeft, cursorTop);
                    Console.Write("{0}/{1} ({2}%)", i, d2oReader.IndexCount,
                        (int) ((i/(double) d2oReader.IndexCount)*100d));
                }

                Console.WriteLine("");
            }

            LoadLangs();
            LoadIcons();
        }

        private static void LoadLangs()
        {
            Database.Database.Execute("DELETE FROM langs");
            Database.Database.Execute("DELETE FROM langs_ui");
            Database.Database.Execute("ALTER TABLE langs_ui AUTO_INCREMENT=1");


            var d2iFiles = new Dictionary<string, D2IFile>();
            string d2iFolder = Path.Combine(FindDofusPath(), "data", "i18n");

            foreach (string file in Directory.EnumerateFiles(d2iFolder, "*.d2i"))
            {
                Match match = Regex.Match(Path.GetFileName(file), @"i18n_(\w+)\.d2i");
                var i18NFile = new D2IFile(file);

                d2iFiles.Add(match.Groups[1].Value, i18NFile);
            }

            var records = new Dictionary<int, LangText>();
            var uiRecords = new Dictionary<string, LangTextUi>();
            foreach (var file in d2iFiles.Where(file => SpecificLanguage.Contains(file.Key)))
            {
                Console.WriteLine("Import {0}...", Path.GetFileName(file.Value.FilePath));
                foreach (var text in file.Value.GetAllText())
                {
                    LangText record;
                    if (!records.ContainsKey(text.Key))
                    {
                        record = new LangText {Id = (uint) text.Key};
                        records.Add(text.Key, record);
                    }
                    else record = records[text.Key];

                    switch (file.Key)
                    {
                        case "fr":
                            record.French = text.Value;
                            break;
                        case "en":
                            record.English = text.Value;
                            break;
                        case "de":
                            record.German = text.Value;
                            break;
                        case "it":
                            record.Italian = text.Value;
                            break;
                        case "es":
                            record.Spanish = text.Value;
                            break;
                        case "ja":
                            record.Japanish = text.Value;
                            break;
                        case "nl":
                            record.Dutsh = text.Value;
                            break;
                        case "pt":
                            record.Portugese = text.Value;
                            break;
                        case "ru":
                            record.Russish = text.Value;
                            break;
                    }
                }

                foreach (var text in file.Value.GetAllUiText())
                {
                    LangTextUi record;
                    if (!uiRecords.ContainsKey(text.Key))
                    {
                        record = new LangTextUi {Name = text.Key};
                        uiRecords.Add(text.Key, record);
                    }
                    else record = uiRecords[text.Key];

                    switch (file.Key)
                    {
                        case "fr":
                            record.French = text.Value;
                            break;
                        case "en":
                            record.English = text.Value;
                            break;
                        case "de":
                            record.German = text.Value;
                            break;
                        case "it":
                            record.Italian = text.Value;
                            break;
                        case "es":
                            record.Spanish = text.Value;
                            break;
                        case "ja":
                            record.Japanish = text.Value;
                            break;
                        case "nl":
                            record.Dutsh = text.Value;
                            break;
                        case "pt":
                            record.Portugese = text.Value;
                            break;
                        case "ru":
                            record.Russish = text.Value;
                            break;
                    }
                }
            }


            int cursorLeft = Console.CursorLeft;
            int cursorTop = Console.CursorTop;
            int i = 0;
            int count = records.Count + uiRecords.Count;

            Console.WriteLine("Save texts(1/2)...");
            foreach (LangText record in records.Values)
            {
                Database.Database.Insert(record);

                i++;
                Console.SetCursorPosition(cursorLeft, cursorTop);
                Console.Write("{0}/{1} ({2}%)", i, count,
                    (int) ((i/(double) count)*100d));
            }

            Console.WriteLine("Save texts(2/2)...");
            cursorLeft = Console.CursorLeft;
            cursorTop = Console.CursorTop;
            foreach (LangTextUi record in uiRecords.Values)
            {
                Database.Database.Insert(record);

                i++;
                Console.SetCursorPosition(cursorLeft, cursorTop);
                Console.Write("{0}/{1} ({2}%)", i, count,
                    (int) ((i/(double) count)*100d));
            }
            Console.WriteLine();
        }

        private static void LoadLangsWithWarning()
        {
            Console.WriteLine("WARNING IT WILL ERASE TABLES 'langs' AND 'langs_ui'. ARE YOU SURE ? (y/n)");
            if (Console.ReadLine() != "y")
                return;

            LoadLangs();
        }

        private static void LoadIconsWithWarnings()
        {
            Console.WriteLine("WARNING IT WILL ERASE TABLE 'icons'. ARE YOU SURE ? (y/n)");
            if (Console.ReadLine() != "y")
                return;

            LoadIcons();
        }

        private static void LoadIcons()
        {
            Database.Database.Execute("DELETE FROM langs");

            string iconsFilePath = Path.Combine(FindDofusPath(), "content", "gfx", "items", "bitmap0.d2p");
            var d2pFile = new D2pFile(iconsFilePath);

            int cursorLeft = Console.CursorLeft;
            int cursorTop = Console.CursorTop;
            int i = 0;
            int count = d2pFile.Entries.Count();
            foreach (D2pEntry entry in d2pFile.Entries)
            {
                var icon = new IconRecord();

                if (!entry.FullFileName.EndsWith(".png"))
                    continue;

                byte[] data = d2pFile.ReadFile(entry);
                string name = entry.FileName.Replace(".png", "");

                int id;
                switch (name)
                {
                    case "empty":
                        id = 0;
                        break;
                    case "error":
                        id = -1;
                        break;
                    default:
                        id = int.Parse(name);
                        break;
                }

                icon.Id = id;
                icon.ImageBinary = data;

                Database.Database.Insert(icon);

                i++;
                Console.SetCursorPosition(cursorLeft, cursorTop);
                Console.Write("{0}/{1} ({2}%)", i, count,
                    (int) ((i/(double) count)*100d));
            }
        }


        private static void GenerateFiles()
        {
            if (!Directory.Exists(FilesOutput))
                Directory.CreateDirectory(FilesOutput);
            else
                foreach (var file in Directory.EnumerateFiles(FilesOutput))
                    File.Delete(file);

            foreach (D2OTable table in m_tables.Values)
            {
                D2OWriter writer;
                if (table.Type.BaseType != typeof (object))
                {
                    var baseTable = table.Type.BaseType.GetCustomAttribute<D2OClassAttribute>().Name;
                    writer =
                        new D2OWriter(Path.Combine(FilesOutput, m_tables[baseTable].TableName + ".d2o"));
                }
                else
                    writer = new D2OWriter(Path.Combine(FilesOutput, table.TableName + ".d2o"));

                Console.WriteLine("Generating {0} ...", Path.GetFileName(writer.Filename));


                var rows = GetTableRows(table);
                writer.StartWriting(false);
                foreach (ID2ORecord row in rows)
                {
                    object obj = row.CreateObject();
                    writer.Write(obj, row.Id);
                }

                writer.EndWriting();
            }
        }

        public static void SyncDatabases()
        {
            var worldDatabase = new DatabaseAccessor(WorldDatabaseConfiguration);

            Console.WriteLine("Connecting to {0}@{1}", WorldDatabaseConfiguration.DbName,
                WorldDatabaseConfiguration.Host);

            worldDatabase.RegisterMappingAssembly(typeof (WorldServer).Assembly);
            worldDatabase.Initialize();
            try
            {
                worldDatabase.OpenConnection();
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not connect : " + e);
                return;
            }

            Console.WriteLine("Connected!");

            var worldTables = EnumerateTables(typeof(WorldServer).Assembly).ToList();
            var monsterGradeTable = worldTables.FirstOrDefault(x => x.ClassName == "MonsterGrade");

            if (monsterGradeTable == null)
            {
                Console.WriteLine("Table associated to class MonsterGrade not found !");
            }

            foreach (var table in worldTables)
            {
                // reset the table
                worldDatabase.Database.Execute("DELETE FROM " + table.TableName);
                worldDatabase.Database.Execute("ALTER TABLE " + table.TableName + " AUTO_INCREMENT=1");

                if (table.TableName == "monsters_grades") // handled by monsters_templates
                    continue;

                Console.WriteLine("Build table '{0}' ...", table.TableName);

                if (!m_tables.ContainsKey(table.ClassName))
                {
                    Console.WriteLine("{0} does not contain a table bound to class {1}", DatabaseConfiguration.DbName, table.ClassName);
                    continue;
                }

                var dataTable = m_tables[table.ClassName];

                using (var transaction = Database.Database.GetTransaction())
                {
                    int cursorLeft = Console.CursorLeft;
                    int cursorTop = Console.CursorTop;
                    int i = 0;
                    var rows = GetTableRows(dataTable);
                    foreach (ID2ORecord row in rows)
                    {
                        IAssignedByD2O record;
                        var obj = row.CreateObject();

                        // monster grades are in an other table
                        var monster = obj as Monster;
                        if (monster != null && monsterGradeTable != null)
                        {
                            foreach (MonsterGrade monsterGrade in monster.grades)
                            {
                                record = (IAssignedByD2O)monsterGradeTable.Constructor.DynamicInvoke();
                                record.AssignFields(monsterGrade);

                                worldDatabase.Database.Insert(record);
                            }
                        }

                        record = (IAssignedByD2O)table.Constructor.DynamicInvoke();
                        record.AssignFields(obj);

                        worldDatabase.Database.Insert(record);

                        i++;
                        Console.SetCursorPosition(cursorLeft, cursorTop);
                        Console.Write("{0}/{1} ({2}%)", i, rows.Count,
                                      (int) ((i/(double) rows.Count)*100d));
                    }
                    Console.SetCursorPosition(cursorLeft, cursorTop);
                    transaction.Complete();
                }
            }
        }

        #endregion

        #region Helpers
        private static IList GetTableRows(D2OTable table)
        {
            MethodInfo method = typeof (Database).GetMethodExt("Fetch", 1, new[] {typeof (Sql)});
            MethodInfo generic = method.MakeGenericMethod(table.Type);
            var rows =
                ((IList)
                    generic.Invoke(Database.Database,
                        new object[] {new Sql("SELECT * FROM `" + table.TableName + "`")}));

            return rows;
        }
        #endregion
    }
}