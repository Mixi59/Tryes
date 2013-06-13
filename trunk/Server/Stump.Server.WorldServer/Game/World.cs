using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Threading;
using NLog;
using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Database;
using Stump.Server.BaseServer.Initialization;
using Stump.Server.BaseServer.Network;
using Stump.Server.WorldServer.Core.Network;
using Stump.Server.WorldServer.Database;
using Stump.Server.WorldServer.Database.Accounts;
using Stump.Server.WorldServer.Database.Interactives;
using Stump.Server.WorldServer.Database.Monsters;
using Stump.Server.WorldServer.Database.Npcs;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Database.World.Maps;
using Stump.Server.WorldServer.Database.World.Triggers;
using Stump.Server.WorldServer.Game.Actors.RolePlay;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Monsters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Merchants;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;
using Stump.Server.WorldServer.Game.Interactives;
using Stump.Server.WorldServer.Game.Maps;
using Stump.Server.WorldServer.Game.Maps.Cells;
using Stump.Server.WorldServer.Game.Maps.Cells.Triggers;
using Stump.Server.WorldServer.Handlers.Basic;
using Stump.Server.WorldServer.Handlers.Chat;

namespace Stump.Server.WorldServer.Game
{
    public class World : DataManager<World>
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        private readonly ConcurrentDictionary<int, Character> m_charactersById =
            new ConcurrentDictionary<int, Character>(Environment.ProcessorCount, ClientManager.MaxConcurrentConnections);

        private readonly ConcurrentDictionary<string, Character> m_charactersByName =
            new ConcurrentDictionary<string, Character>(Environment.ProcessorCount, ClientManager.MaxConcurrentConnections);

        private readonly Dictionary<int, WorldAccount> m_connectedAccounts =
            new Dictionary<int, WorldAccount>();

        private Dictionary<int, Area> m_areas = new Dictionary<int, Area>();

        private int m_characterCount;

        private Dictionary<int, Map> m_maps = new Dictionary<int, Map>();
        private Dictionary<int, SubArea> m_subAreas = new Dictionary<int, SubArea>();
        private Dictionary<int, SuperArea> m_superAreas = new Dictionary<int, SuperArea>();

        private readonly object m_saveLock = new object();
        private readonly ConcurrentBag<ISaveable> m_saveablesInstances = new ConcurrentBag<ISaveable>();

        public event Action<Character> CharacterJoined;

        private void OnCharacterEntered(Character character)
        {
            Action<Character> handler = CharacterJoined;
            if (handler != null) handler(character);
        }

        public event Action<Character> CharacterLeft;

        private void OnCharacterLeft(Character character)
        {
            Action<Character> handler = CharacterLeft;
            if (handler != null) handler(character);
        }

        public int CharacterCount
        {
            get { return m_characterCount; }
        }

        public object SaveLock
        {
            get { return m_saveLock; }
        }

        #region Initialization

        private bool m_spacesLoaded;
        private bool m_spacesSpawned;

        [Initialization(InitializationPass.Seventh)]
        public override void Initialize()
        {
            // maps
            LoadSpaces();
            SpawnSpaces();
        }

        public void LoadSpaces()
        {
            if (m_spacesLoaded)
            {
                UnSetLinks();
            }

            m_spacesLoaded = true;

            logger.Info("Load maps...");
            m_maps = Database.Query<MapRecord, MapPositionRecord, MapRecord>
                (new MapRecordRelator().Map, MapRecordRelator.FetchQuery).ToDictionary(entry => entry.Id, entry => new Map(entry));

            logger.Info("Load sub areas...");
            m_subAreas = Database.Query<SubAreaRecord>(SubAreaRecordRelator.FetchQuery).ToDictionary(entry => entry.Id, entry => new SubArea(entry));

            logger.Info("Load areas...");
            m_areas = Database.Query<AreaRecord>(AreaRecordRelator.FetchQuery).ToDictionary(entry => entry.Id, entry => new Area(entry));

            logger.Info("Load super areas...");
            m_superAreas = Database.Query<SuperAreaRecord>(SuperAreaRecordRelator.FetchQuery).ToDictionary(entry => entry.Id, entry => new SuperArea(entry));

            SetLinks();

        }

        public void SpawnSpaces()
        {
            if (m_spacesSpawned)
            {
                UnSpawnSpaces();
            }

            m_spacesSpawned = true;

            logger.Info("Spawn npcs ...");
            SpawnNpcs();

            logger.Info("Spawn interactives ...");
            SpawnInteractives();

            logger.Info("Spawn cell triggers ...");
            SpawnCellTriggers();

            logger.Info("Spawn monsters ...");
            SpawnMonsters();

            logger.Info("Spawn merchants ...");
            SpawnMerchants();
        }

        private void SetLinks()
        {
            foreach (Map map in m_maps.Values)
            {
                if (map.Record.Position == null)
                    continue;

                SubArea subArea;
                if (m_subAreas.TryGetValue(map.Record.Position.SubAreaId, out subArea))
                {
                    subArea.AddMap(map);
                }
            }

            foreach (SubArea subArea in m_subAreas.Values)
            {
                Area area;
                if (m_areas.TryGetValue(subArea.Record.AreaId, out area))
                {
                    area.AddSubArea(subArea);
                }
            }

            foreach (Area area in m_areas.Values)
            {
                SuperArea superArea;
                if (m_superAreas.TryGetValue(area.Record.SuperAreaId, out superArea))
                {
                    superArea.AddArea(area);
                }
            }
        }

        private void UnSetLinks()
        {
            foreach (Map map in m_maps.Values)
            {
                if (map.Record.Position == null)
                    continue;

                SubArea subArea;
                if (m_subAreas.TryGetValue(map.Record.Position.SubAreaId, out subArea))
                {
                    subArea.RemoveMap(map);
                }
            }

            foreach (SubArea subArea in m_subAreas.Values)
            {
                Area area;
                if (m_areas.TryGetValue(subArea.Record.AreaId, out area))
                {
                    area.RemoveSubArea(subArea);
                }
            }

            foreach (Area area in m_areas.Values)
            {
                SuperArea superArea;
                if (m_superAreas.TryGetValue(area.Record.SuperAreaId, out superArea))
                {
                    superArea.RemoveArea(area);
                }
            }
        }

        private void SpawnNpcs()
        {
            foreach (NpcSpawn npcSpawn in NpcManager.Instance.GetNpcSpawns())
            {
                ObjectPosition position = npcSpawn.GetPosition();

                position.Map.SpawnNpc(npcSpawn);
            }
        }

        public void UnSpawnSpaces()
        {
            foreach (var map in m_maps.Values)
            {
                var interactives = map.GetInteractiveObjects().ToArray();

                foreach (var interactive in interactives)
                {
                    map.UnSpawnInteractive(interactive);
                }

                foreach (var pool in map.SpawningPools.ToArray())
                {
                    map.RemoveSpawningPool(pool);
                }

                var triggers = map.GetTriggers().ToArray();

                foreach (var trigger in triggers)
                {
                    map.RemoveTrigger(trigger);
                }
            }

            foreach (var subArea in m_subAreas)
            {
                foreach (var monsterSpawn in subArea.Value.MonsterSpawns)
                {
                    subArea.Value.RemoveMonsterSpawn(monsterSpawn);
                }
            }
        }

        private void SpawnInteractives()
        {
            foreach (InteractiveSpawn interactive in InteractiveManager.Instance.GetInteractiveSpawns())
            {
                Map map = interactive.GetMap();

                if (map == null)
                {
                    logger.Error("Cannot spawn interactive id={0} : map {1} doesn't exist", interactive.Id, interactive.MapId);
                    continue;
                }

                map.SpawnInteractive(interactive);
            }
        }

        private void SpawnCellTriggers()
        {
            foreach (CellTriggerRecord cellTrigger in CellTriggerManager.Instance.GetCellTriggers())
            {
                CellTrigger trigger = cellTrigger.GenerateTrigger();

                trigger.Position.Map.AddTrigger(trigger);
            }
        }

        private void SpawnMonsters()
        {
            foreach (MonsterSpawn spawn in MonsterManager.Instance.GetMonsterSpawns())
            {
                if (spawn.Map != null)
                {
                    spawn.Map.AddMonsterSpawn(spawn);
                }
                else if (spawn.SubArea != null)
                {
                    spawn.SubArea.AddMonsterSpawn(spawn);
                }
            }

            foreach (var spawn in MonsterManager.Instance.GetMonsterDungeonsSpawns())
            {
                if (spawn.Map != null)
                {
                    spawn.Map.AddMonsterDungeonSpawn(spawn);
                }
            }

            foreach (var map in m_maps)
            {
                if (map.Value.MonsterSpawnsCount > 0)
                    map.Value.EnableClassicalMonsterSpawns();
            }
        }

        private void SpawnMerchants()
        {
            foreach (WorldMapMerchantRecord spawn in MerchantManager.Instance.GetMerchantSpawns())
            {
                if (spawn.Map != null)
                {
                    spawn.Map.AddMerchantSpawn(spawn);
                }
            }
        }

        #endregion

        #region Maps

        public Map GetMap(int id)
        {
            Map map;
            m_maps.TryGetValue(id, out map);

            return map;
        }

        public Map GetMap(int x, int y, bool outdoor = true)
        {
            return m_maps.Values.Where(entry => entry.Position.X == x && entry.Position.Y == y && entry.Outdoor == outdoor).FirstOrDefault();
        }

        public IEnumerable<Map> GetMaps()
        {
            return m_maps.Values;
        }

        public Map[] GetMaps(int x, int y)
        {
            return m_maps.Values.Where(entry => entry.Position.X == x && entry.Position.Y == y).ToArray();
        }

        public Map[] GetMaps(int x, int y, bool outdoor)
        {
            return m_maps.Values.Where(entry => entry.Position.X == x && entry.Position.Y == y && entry.Outdoor == outdoor).ToArray();
        }

        public Map[] GetMaps(Map reference, int x, int y)
        {
            var maps = reference.SubArea.GetMaps(x, y);
            if (maps.Length > 0)
                return maps;

            maps = reference.Area.GetMaps(x, y);
            if (maps.Length > 0)
                return maps;

            maps = reference.SuperArea.GetMaps(x, y);
            if (maps.Length > 0)
                return maps;

            return new Map[0];
        }

        public Map[] GetMaps(Map reference, int x, int y, bool outdoor)
        {
            var maps = reference.SubArea.GetMaps(x, y, outdoor);
            if (maps.Length > 0)
                return maps;

            maps = reference.Area.GetMaps(x, y, outdoor);
            if (maps.Length > 0)
                return maps;

            maps = reference.SuperArea.GetMaps(x, y, outdoor);
            if (maps.Length > 0)
                return maps;

            return new Map[0];
        }

        public SubArea GetSubArea(int id)
        {
            SubArea subArea;
            m_subAreas.TryGetValue(id, out subArea);

            return subArea;
        }

        public SubArea GetSubArea(string name)
        {
            return m_subAreas.Values.FirstOrDefault(entry => entry.Record.Name == name);
        }

        public Area GetArea(int id)
        {
            Area area;
            m_areas.TryGetValue(id, out area);

            return area;
        }

        public Area GetArea(string name)
        {
            return m_areas.Values.FirstOrDefault(entry => entry.Name == name);
        }

        public SuperArea GetSuperArea(int id)
        {
            SuperArea superArea;
            m_superAreas.TryGetValue(id, out superArea);

            return superArea;
        }
        public SuperArea GetSuperArea(string name)
        {
            return m_superAreas.Values.FirstOrDefault(entry => entry.Name == name);
        }

        #endregion

        #region Actors

        public void Enter(Character character)
        {
            // note : to delete
            if (m_charactersById.ContainsKey(character.Id))
                Leave(character);

            if (m_charactersById.TryAdd(character.Id, character) && m_charactersByName.TryAdd(character.Name, character))
            {
                Interlocked.Increment(ref m_characterCount);
                OnCharacterEntered(character);
            }
            else
                logger.Error("Cannot add character {0} to the World", character);

            if (!m_connectedAccounts.ContainsKey(character.Account.Id))
                m_connectedAccounts.Add(character.Account.Id, character.Client.WorldAccount);
        }

        public void Leave(Character character)
        {
            Character dummy;
            if (m_charactersById.TryRemove(character.Id, out dummy) && m_charactersByName.TryRemove(character.Name, out dummy))
            {
                Interlocked.Decrement(ref m_characterCount);
                OnCharacterLeft(character);
            }
            else
                logger.Error("Cannot remove character {0} from the World", character);

            m_connectedAccounts.Remove(character.Account.Id);
        }

        public bool IsConnected(int id)
        {
            return m_charactersById.ContainsKey(id);
        }

        public bool IsConnected(string name)
        {
            return m_charactersByName.ContainsKey(name);
        }

        public bool IsAccountConnected(int id)
        {
            return m_connectedAccounts.ContainsKey(id);
        }

        public WorldAccount GetConnectedAccount(int id)
        {
            WorldAccount account;
            return m_connectedAccounts.TryGetValue(id, out account) ? account : null;
        }

        public Character GetCharacter(int id)
        {
            Character character;
            return m_charactersById.TryGetValue(id, out character) ? character : null;
        }

        public Character GetCharacter(string name)
        {
            Character character;
            return m_charactersByName.TryGetValue(name, out character) ? character : null;
        }

        public Character GetCharacter(Predicate<Character> predicate)
        {
            return m_charactersById.FirstOrDefault(k => predicate(k.Value)).Value;
        }

        public IEnumerable<Character> GetCharacters(Predicate<Character> predicate)
        {
            return m_charactersById.Values.Where(k => predicate(k));
        }

        /// <summary>
        /// Get a spell by a search pattern. *account = current spell used by account, name = spell by his name.
        /// </summary>
        /// <returns></returns>
        public Character GetCharacterByPattern(string pattern)
        {
            if (pattern[0] == '*')
            {
                string name = pattern.Remove(0, 1);


                return ClientManager.Instance.FindAll<WorldClient>(entry => entry.Account.Login == name).
                    Select(entry => entry.Character).SingleOrDefault();
            }

            return GetCharacter(pattern);
        }

        /// <summary>
        /// Get a spell by a search pattern. * = caller, *account = current spell used by account, name = spell by his name.
        /// </summary>
        /// <returns></returns>
        public Character GetCharacterByPattern(Character caller, string pattern)
        {
            if (pattern == "*")
                return caller;

            return GetCharacterByPattern(pattern);
        }

        public void ForEachCharacter(Action<Character> action)
        {
            foreach (var key in m_charactersById)
                action(key.Value);
        }

        public void ForEachCharacter(Predicate<Character> predicate, Action<Character> action)
        {
            foreach (var key in m_charactersById.Where(k => predicate(k.Value)))
                action(key.Value);
        }

        public void SendAnnounce(string announce)
        {
            WorldServer.Instance.IOTaskPool.AddMessage(() => ForEachCharacter(character => character.SendServerMessage(announce)));
        }
        
        public void SendAnnounce(string announce, Color color)
        {
            WorldServer.Instance.IOTaskPool.AddMessage(() => ForEachCharacter(character => character.SendServerMessage(announce, color)));
        }

        #endregion


        public void RegisterSaveableInstance(ISaveable instance)
        {
            m_saveablesInstances.Add(instance);
        }

        public void Save()
        {
            lock (SaveLock)
            {
                logger.Info("Saving world ...");
                var sw = Stopwatch.StartNew();

                var clients = ClientManager.Instance.FindAll<WorldClient>();

                foreach (var client in clients)
                {
                    try
                    {
                        if (client.Character != null)
                        {
                            client.Character.SaveNow();
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error("Cannot save {0} : {1}", client, ex);
                    }
                }

                foreach (var instance in m_saveablesInstances)
                {
                    try
                    {
                        instance.Save();
                    }
                    catch (Exception ex)
                    {
                        logger.Error("Cannot save {0} : {1}", instance, ex);
                    }
                }

                logger.Info("World server saved ! ({0} ms)", sw.ElapsedMilliseconds);
            }
        }

        public void Stop()
        {
            foreach (var area in m_areas)
            {
                area.Value.Stop();
            }
        }

        private List<Area> m_pausedAreas = new List<Area>();
        /// <summary>
        /// Have to be called from another thread !!
        /// </summary>
        public void Pause()
        {
            logger.Info("World Paused !!");
            foreach (var area in m_areas)
            {
                if (area.Value.IsInContext)
                    throw new Exception("Has to be called from another thread !!");

                area.Value.Stop(true);
                m_pausedAreas.Add(area.Value);
            }

            if (WorldServer.Instance.IOTaskPool.IsInContext)
                throw new Exception("Has to be called from another thread !!");

            WorldServer.Instance.IOTaskPool.Stop(true);
        }

        public void Resume()
        {
            logger.Info("World Resumed");
            foreach (var pausedArea in m_pausedAreas)
            {
                pausedArea.Start();
            }

            m_pausedAreas.Clear();


            WorldServer.Instance.IOTaskPool.Start();
        }
    }
}