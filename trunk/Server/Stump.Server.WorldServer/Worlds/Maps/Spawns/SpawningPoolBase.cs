using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading;
using Stump.Core.Timers;
using Stump.Server.WorldServer.Worlds.Actors.RolePlay;
using Stump.Server.WorldServer.Worlds.Actors.RolePlay.Monsters;

namespace Stump.Server.WorldServer.Worlds.Maps.Spawns
{
    public enum SpawningPoolState
    {
        Stoped,
        Running,
        Paused
    }

    public abstract class SpawningPoolBase
    {
        protected SpawningPoolBase(Map map)
        {
            Map = map;
            Map.ActorLeave += OnMapActorLeave;
            Spawns = new List<MonsterGroup>();
        }

        protected SpawningPoolBase(Map map, int interval)
            : this(map)
        {
            Interval = interval;
        }

        public Map Map
        {
            get;
            protected set;
        }

        public int Interval
        {
            get;
            protected set;
        }

        protected List<MonsterGroup> Spawns
        {
            get;
            set;
        }

        public int SpawnsCount
        {
            get { return Spawns.Count; }
        }

        protected MonsterGroup NextGroup
        {
            get;
            set;
        }

        protected TimerEntry SpawnTimer
        {
            get;
            set;
        }

        public SpawningPoolState State
        {
            get;
            private set;
        }

        public bool AutoSpawnEnabled
        {
            get { return State != SpawningPoolState.Stoped; }
        }

        public void StartAutoSpawn()
        {
            lock (this)
            {
                if (AutoSpawnEnabled)
                    return;


                ResetTimer();
                State = SpawningPoolState.Running;
            }
        }

        public void StopAutoSpawn()
        {
            lock (this)
            {
                if (!AutoSpawnEnabled)
                    return;


                if (SpawnTimer != null)
                    SpawnTimer.Dispose();

                State = SpawningPoolState.Stoped;
            }
        }

        protected void PauseAutoSpawn()
        {
            lock (this)
            {
                if (State != SpawningPoolState.Running)
                    return;

                if (SpawnTimer != null)
                    SpawnTimer.Dispose();

                State = SpawningPoolState.Paused;
            }
        }

        protected void ResumeAutoSpawn()
        {
            lock (this)
            {
                if (State != SpawningPoolState.Paused)
                    return;

                ResetTimer();
                State = SpawningPoolState.Running;
            }
        }

        private void TimerCallBack()
        {
            lock (this)
            {
                SpawnNextGroup();
            }

            if (IsLimitReached())
                PauseAutoSpawn();
            else
                ResetTimer();
        }

        private void ResetTimer()
        {
            SpawnTimer = Map.Area.CallDelayed(GetNextSpawnInterval(), TimerCallBack);
        }

        public void SpawnNextGroup()
        {
            MonsterGroup group = DequeueNextGroupToSpawn();

            if (group == null)
                return;

            Map.Enter(group);

            OnGroupSpawned(group);
        }

        public void SetTimer(int time)
        {
            lock (this)
            {
                Interval = time;

                ResetTimer();
            }
        }

        protected abstract bool IsLimitReached();
        protected abstract int GetNextSpawnInterval();

        protected virtual MonsterGroup DequeueNextGroupToSpawn()
        {
            if (NextGroup != null)
            {
                return NextGroup;
            }

            return null;
        }

        public virtual void SetNextGroupToSpawn(IEnumerable<Monster> monsters)
        {
            Contract.Requires(monsters.Any());

            NextGroup = new MonsterGroup(Map.GetNextContextualId(), Map.GetRanomFreePosition());

            foreach (Monster monster in monsters)
            {
                NextGroup.AddMonster(monster);
            }
        }


        private void OnMapActorLeave(Map map, RolePlayActor actor)
        {
            if (actor is MonsterGroup && (Spawns.Contains(actor as MonsterGroup)))
                OnGroupUnSpawned(actor as MonsterGroup);
        }

        public event Action<SpawningPoolBase, MonsterGroup> Spawned;

        protected virtual void OnGroupSpawned(MonsterGroup group)
        {
            lock (Spawns)
                Spawns.Add(group);

            NextGroup = null;

            Action<SpawningPoolBase, MonsterGroup> handler = Spawned;
            if (handler != null)
                handler(this, group);
        }

        protected virtual void OnGroupUnSpawned(MonsterGroup monster)
        {
            Contract.Requires(Spawns.Contains(monster));

            lock (Spawns)
                Spawns.Remove(monster);

            if (!IsLimitReached() && State == SpawningPoolState.Paused)
                ResumeAutoSpawn();
        }
    }
}