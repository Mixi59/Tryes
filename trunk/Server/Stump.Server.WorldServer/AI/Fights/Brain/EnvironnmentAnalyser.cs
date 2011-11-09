using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Server.WorldServer.AI.Fights.Actions;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Worlds.Actors.Fight;
using Stump.Server.WorldServer.Worlds.Fights;
using Stump.Server.WorldServer.Worlds.Maps.Cells;
using Stump.Server.WorldServer.Worlds.Maps.Cells.Shapes;
using Stump.Server.WorldServer.Worlds.Spells;

namespace Stump.Server.WorldServer.AI.Fights.Brain
{
    public class EnvironnmentAnalyser
    {
        public EnvironnmentAnalyser(AIFighter fighter)
        {
            Fighter = fighter;
            CellInformationProvider = new AIFightCellsInformationProvider(Fighter.Fight, Fighter);
        }

        public AIFightCellsInformationProvider CellInformationProvider
        {
            get;
            private set;
        }

        public AIFighter Fighter
        {
            get;
            private set;
        }

        public Fight Fight
        {
            get { return Fighter.Fight; }
        }

        public Cell GetCellToCastSpell(FightActor target, Spell spell)
        {
            var cell = target.Position.Point.GetAdjacentCells(CellInformationProvider.IsCellWalkable).OrderBy(entry => entry.DistanceTo(Fighter.Position.Point)).FirstOrDefault();

            if (cell == null)
                return default(Cell);

            return CellInformationProvider.GetCellInformation(cell.CellId).Cell;
        }

        public Cell GetCellToFlee()
        {
            var movementsCells = GetMovementCells();
            var fighters = Fight.GetAllFighters(entry => entry.IsEnnemyWith(Fighter));

            var betterCell = default(Cell);
            long betterCellIndice = 0;
            for (int i = 0; i < movementsCells.Length; i++)
            {
                if (!CellInformationProvider.IsCellWalkable(movementsCells[i].Id))
                    continue;

                long indice = fighters.Sum(entry => entry.Position.Point.DistanceTo(new MapPoint(movementsCells[i])));

                if (betterCellIndice < indice)
                {
                    betterCellIndice = indice;
                    betterCell = movementsCells[i];
                }
            }

            return betterCell;
        }

        public Cell[] GetMovementCells()
        {
            return GetMovementCells(Fighter.MP);
        }

        public Cell[] GetMovementCells(int mp)
        {
            var circle = new Lozenge(0, (uint) mp);

            return circle.GetCells(Fighter.Cell, Fight.Map);
        }

        public FightActor GetNearestFighter()
        {
            return GetNearestFighter(entry => true);
        }

        public FightActor GetNearestAlly()
        {
            return GetNearestFighter(entry => entry.IsFriendlyWith(Fighter));
        }

        public FightActor GetNearestEnnemy()
        {
            return GetNearestFighter(entry => entry.IsEnnemyWith(Fighter));
        }

        public FightActor GetNearestFighter(Predicate<FightActor> predicate)
        {
            return Fight.GetAllFighters(predicate).OrderBy(entry => entry.Position.Point.DistanceTo(Fighter.Position.Point)).FirstOrDefault();
        }
    }
}