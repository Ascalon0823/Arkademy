using CGS;
using CGS.Grid;
using UnityEngine;
namespace Arkademy
{
    public struct StageTile
    {
        public enum Type
        {
            Void,
            Wall,
            Floor
        }
        public Type TileType;

    }
    public class Stage : SquareGrid2D<StageTile>
    {
        public Stage(int x, int y, Vector3 anchorPos, Vector3 cellSize) : base(x, y, anchorPos, cellSize)
        {
        }

        public Stage(int x, int y, StageTile val, Vector3 anchorPos, Vector3 cellSize) : base(x, y, val, anchorPos, cellSize)
        {
        }

        public Stage(StageTile[,] d, Vector3 anchorPos, Vector3 cellSize) : base(d, anchorPos, cellSize)
        {
        }
    }
}