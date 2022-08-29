using CGS.Grid;
using UnityEngine;

namespace Arkademy
{
    public struct WorldTile
    {
        public int Altitude;
    }

    public class World : SquareGrid2D<WorldTile>
    {
        public World(int x, int y, Vector3 anchorPos, Vector3 cellSize) : base(x, y, anchorPos, cellSize)
        {
        }

        public World(int x, int y, WorldTile val, Vector3 anchorPos, Vector3 cellSize) : base(x, y, val, anchorPos,
            cellSize)
        {
        }

        public World(WorldTile[,] d, Vector3 anchorPos, Vector3 cellSize) : base(d, anchorPos, cellSize)
        {
        }

        public static World Create(int size)
        {
            return new World(size, size, Vector3.zero, new Vector3(1, 0, 1));
        }
    }
}