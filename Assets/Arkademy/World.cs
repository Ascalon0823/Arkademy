using System.Collections.Generic;
using CGS.Grid;
using UnityEngine;

namespace Arkademy
{
    public struct WorldTile
    {
        public int Altitude;
        public int TectonicIdx;
        public bool TectonicEdge;
        public World.TectonicPlate.EdgeType EdgeType;
    }

    public class World : SquareGrid2D<WorldTile>
    {
        public struct TectonicPlate
        {
            public enum EdgeType
            {
                None,
                Collision,
                Subduction,
                Divergent,
                Shear,
                Static
            }

            public Vector2Int Origin;
            public List<Vector2Int> Edges;
            public Vector2 Direction;
            public int Density;
        }

        public readonly List<TectonicPlate> TectonicPlates = new List<TectonicPlate>();

        public void UpdateTile(Vector2Int coord, System.Func<WorldTile, Vector2Int, WorldTile> updateFunc)
        {
            var tile = this[coord.x, coord.y];
            this[coord.x, coord.y] = updateFunc(tile, coord);
        }

        public void UpdateTile(int x, int y, System.Func<WorldTile, int, int, WorldTile> updateFunc)
        {
            var tile = this[x, y];
            this[x, y] = updateFunc(tile, x, y);
        }

        public void IterateAndUpdate(System.Func<int, int, WorldTile, WorldTile> func)
        {
            Iterate((x, y) => { this[x, y] = func(x, y, this[x, y]); });
        }

        public TectonicPlate GetPlateByCoord(int x, int y)
        {
            return TectonicPlates[this[x, y].TectonicIdx];
        }

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
            return Create(size, size);
        }

        public static World Create(int sizeX, int sizeY)
        {
            return new World(sizeX, sizeY, Vector3.zero + new Vector3(-sizeX / 2f, -sizeY / 2f), new Vector3(1, 1, 0));
        }
    }
}