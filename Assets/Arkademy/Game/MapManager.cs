using System;
using UnityEngine;

namespace Arkademy.Game
{
    public class MapManager : MonoBehaviour
    {
        public static MapManager Instance;
        public Map map => _map;
        private Map _map;
        [SerializeField] private int size_x;
        [SerializeField] private int size_y;
        [SerializeField] private MapCell defaultCell;
        [SerializeField] private Vector3 anchor;
        [SerializeField] private Vector3 cellSize;
        [SerializeField] private MapBuilder mapBuilder;
        public void BuildMap()
        {
            Debug.Log($"Building map\nSize: {size_x} {size_y}\nAnchor {anchor}\nCellSize: {cellSize}");
            if (cellSize.sqrMagnitude == 0)
            {
                Debug.LogWarning("Failed to build map");
                return;
            }
            _map =new Map(size_x, size_y,defaultCell,anchor,cellSize);
            mapBuilder.BuildMap(_map);
        }

        private void OnDrawGizmosSelected()
        {
            _map?.Iterate((i, j) =>
            {
                if (!map[i,j].walkable) return;
                Gizmos.DrawWireCube(_map.GetWorldPos(i,j),_map.CellSize);
            });
        }
    }
}