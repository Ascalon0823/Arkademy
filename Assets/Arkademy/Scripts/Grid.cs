using System;
using System.Collections.Generic;
using UnityEngine;

namespace Arkademy
{
    public enum SqrDir
    {
        Up = 0,
        UpRight = 1,
        Right = 2,
        DownRight = 3,
        Down = 4,
        DownLeft = 5,
        Left = 6,
        UpLeft = 7
    }

    public static class SqrDirExt
    {
        private static readonly SqrDir[] ClockWise =
        {
            SqrDir.Up, SqrDir.UpRight, SqrDir.Right, SqrDir.DownRight,
            SqrDir.Down, SqrDir.DownLeft, SqrDir.Left, SqrDir.UpLeft,
            SqrDir.Up
        };

        private static readonly SqrDir[] AntiClockWise =
        {
            SqrDir.Up, SqrDir.UpLeft, SqrDir.Left, SqrDir.DownLeft,
            SqrDir.Down, SqrDir.DownRight, SqrDir.Right, SqrDir.UpRight,
            SqrDir.Up
        };

        private static readonly Vector2[] Vectors =
        {
            new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 0), new Vector2(1, -1),
            new Vector2(0, -1), new Vector2(-1, -1), new Vector2(-1, 0), new Vector2(-1, 1)
        };

        public static Vector2 Vec(this SqrDir dir)
        {
            return Vectors[(int) dir];
        }

        public static int XDir(this SqrDir dir)
        {
            return (int) dir.Vec().x;
        }

        public static int YDir(this SqrDir dir)
        {
            return (int) dir.Vec().y;
        }

        public static SqrDir Next(this SqrDir dir)
        {
            return ClockWise[(int) dir + 1];
        }

        public static SqrDir Prev(this SqrDir dir)
        {
            return AntiClockWise[(int) dir + 1];
        }
        public static SqrDir Opposite(this SqrDir dir)
        {
            return (SqrDir) (((int) dir + 4) % 8);
        }
    }

    public class Grid<T>
    {
        private readonly T[,] _grid;
        private readonly int _x;
        private readonly int _y;

        public Grid(int x, int y)
        {
            _x = x;
            _y = y;
            _grid = new T[x, y];
        }

        public Grid(int x, int y, T fill)
        {
            _x = x;
            _y = y;
            _grid = new T[x, y];
            for (var i = 0; i < x; i++)
            for (var j = 0; j < y; j++)
            {
                _grid[i, j] = fill;
            }
        }

        public Grid(int x, int y, System.Action<T[,]> fillAlgo)
        {
            _x = x;
            _y = y;
            _grid = new T[x, y];
            fillAlgo?.Invoke(_grid);
        }

        public T this[int i, int j]
        {
            get =>  _grid[i, j];
            set => _grid[i, j] = value;
        }


        public List<T> GetNeighbourOf(int i, int j, bool include = false)
        {
            var result = new List<T>();
            if(include&&ValidIdx(i,j))result.Add(_grid[i,j]);
            for (var dir = SqrDir.Up; dir <= SqrDir.UpLeft; dir++)
            {
                var x = i + dir.XDir();
                var y = j + dir.YDir();
                if (ValidIdx(x, y)) result.Add(_grid[x, y]);
            }
            return result;
        }

        public bool ValidIdx(int i, int j)
        {
            return i >= 0 && i < _x && j >= 0 && j < _y;
        }
    }
}