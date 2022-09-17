﻿using System.Collections.Generic;
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
            return new World(size, size, Vector3.zero + new Vector3(-size / 2f, -size / 2f), new Vector3(1, 1, 0));
        }
    }
}