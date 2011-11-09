using System;
using System.Collections.Generic;
using Stump.Server.WorldServer.Database.World;

namespace Stump.Server.WorldServer.Worlds.Maps.Cells.Shapes
{
    public class Square : IShape
    {
        public Square(uint minRadius, uint radius)
        {
            MinRadius = minRadius;
            Radius = radius;
        }

        public bool DiagonalFree
        {
            get;
            set;
        }

        #region IShape Members

        public uint Surface
        {
            get
            {
                return (Radius * 2 + 1) * (Radius * 2 + 1);
            }
        }

        public uint MinRadius
        {
            get;
            set;
        }

        public DirectionsEnum Direction
        {
            get;
            set;
        }

        public uint Radius
        {
            get;
            set;
        }

        public Cell[] GetCells(Cell centerCell, Map map)
        {
            var centerPoint = new MapPoint(centerCell);
            var result = new List<Cell>();

            if (Radius == 0)
            {
                if (MinRadius == 0 && !DiagonalFree)
                    result.Add(centerCell);

                return result.ToArray();
            }

            int x = (int)( centerPoint.X - Radius );
            int y;
            while (x <= centerPoint.X + Radius)
            {
                y = (int) (centerPoint.Y - Radius);
                while (y <= centerPoint.Y - Radius)
                {
                    if (MinRadius == 0 || Math.Abs(centerPoint.X - x) + Math.Abs(centerPoint.Y - y) >= MinRadius)
                        if (!DiagonalFree || Math.Abs(centerPoint.X - x) != Math.Abs(centerPoint.Y - y))
                             AddCellIfValid(x, y, map, result);

                    y++;
                }

                x++;
            }

            return result.ToArray();
        }

        private static void AddCellIfValid(int x, int y, Map map, IList<Cell> container)
        {
            if (!MapPoint.IsInMap(x, y))
                return;

            container.Add(map.Cells[MapPoint.CoordToCellId(x, y)]);
        }
        #endregion
    }
}