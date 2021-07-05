using UnityEngine;

namespace Arkademy.Game
{
    [System.Serializable]
    public struct MapCell
    {
        public bool walkable;
    }

    [System.Serializable]
    public class Map : Grid<MapCell>
    {
        /// <summary>
        /// Center of the Map
        /// </summary>
        public Vector3 Anchor => anchor;
        [SerializeField] private Vector3 anchor;
        public Vector3 CellSize => cellSize;
        [SerializeField] private Vector3 cellSize;

        public Map(int x, int y, Vector3 anchor, Vector3 cellSize) : base(x, y)
        {
            this.anchor = anchor;
            this.cellSize = cellSize;
        }

        public Map(int x, int y, MapCell val, Vector3 anchor, Vector3 cellSize) : base(x, y, val)
        {
            this.anchor = anchor;
            this.cellSize = cellSize;
        }

        public Map(MapCell[,] d, Vector3 anchor, Vector3 cellSize) : base(d)
        {
            this.anchor = anchor;
            this.cellSize = cellSize;
        }
        public Vector3 Extend()
        {
            return new Vector3(Width()*cellSize.x/2f,0f,Height()*cellSize.z/2f);
        }
   
        public Vector3 CellExtend()
        {
            return cellSize / 2f;
        }
        public Vector3 GetWorldPos(int x, int y)
        {
            return anchor - Extend() + new Vector3(x * cellSize.x, 0f, y * cellSize.z) + CellExtend();
        }

        public Vector2 FromWorldPos(Vector3 pos)
        {
            return Vector2.one;
        }

        public MapCell GetCellFromWorldPos(Vector3 pos)
        {
            return default;
        }
    }
}