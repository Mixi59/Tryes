using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Types;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Maps;
using Stump.Server.WorldServer.Game.Maps.Cells;

namespace Stump.Server.WorldServer.Game.Actors.RolePlay
{
    public abstract class RolePlayActor : ContextActor
    {
        #region Network

        public override GameContextActorInformations GetGameContextActorInformations()
        {
            return new GameRolePlayActorInformations(Id, Look, GetEntityDispositionInformations());
        }

        #endregion

        #region Actions

        #region Teleport

        public bool Teleport(MapNeighbour mapNeighbour)
        {
            var neighbour = Position.Map.GetNeighbouringMap(mapNeighbour);

            if (neighbour == null)
                return false;

            var destination = new ObjectPosition(neighbour,
                Position.Map.GetCellAfterChangeMap(Position.Cell.Id, mapNeighbour), Position.Direction);

            return Teleport(destination);
        }

        public bool Teleport(Map map, Cell cell)
        {
            return Teleport(new ObjectPosition(map, cell));
        }

        public virtual bool Teleport(ObjectPosition destination)
        {
            if (IsMoving())
                StopMove();

            if (!CanChangeMap())
                return false;

            if (Position.Map == destination.Map)
                return MoveInstant(destination);

            NextMap = destination.Map;
            LastMap = Map;

            Position.Map.Leave(this);
            Position = destination.Clone();
            Position.Map.Enter(this);

            NextMap = null;
            LastMap = null;

            OnTeleported(Position);

            return true;
        }

        public virtual bool CanChangeMap()
        {
            return Map != null && Map.IsActor(this);
        }

        #endregion

        #endregion
    }
}