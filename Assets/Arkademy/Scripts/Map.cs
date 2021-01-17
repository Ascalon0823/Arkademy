using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arkademy
{
    public struct NavNode
    {
        public string type;
        public bool walkable;
        public float cost;
    }
    public class Map : MonoBehaviour
    {
        private static readonly NavNode Plain = new NavNode
        {
            type = "Plain",
            walkable = true,
            cost = 1
        };

        public Grid<NavNode> NavMap => _navMap;
        private Grid<NavNode> _navMap;
        public System.Action onMapBuilt;
        public const float CellSize = 1f;
        public static float HalfSize => CellSize / 2f;
        public int Width => width;
        public int Height => height;
        [SerializeField] private int width, height;

        private void Start()
        {
            BuildMap(width,height);
        }

        private void BuildMap(int x, int y)
        {
            _navMap = new Grid<NavNode>(x,y,Plain);
            onMapBuilt?.Invoke();
        }

        public (int x, int y) WorldToGridCoord(Vector3 worldPos)
        {
            return (Mathf.FloorToInt(worldPos.x/CellSize),Mathf.FloorToInt(worldPos.z / CellSize));
        }

        public Vector3 GetCenterOfCell(int x, int y, int height = 0)
        {
            return new Vector3(x + HalfSize,height,y + HalfSize);
        }
    }
}
