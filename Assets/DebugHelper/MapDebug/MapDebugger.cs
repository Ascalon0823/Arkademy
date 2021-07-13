using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Arkademy.Game;

namespace DebugHelper.MapDebug
{
    public class MapDebugger : MonoBehaviour
    {
        [SerializeField] private MapManager map;
#if !UNITY_EDITOR
        private void Awake()
        {
            Destroy(gameObject);
        }
#endif
#if UNITY_EDITOR
        private void Update()
        {
            if (!map || map.map == null)
            {
                return;
            }

            var pos = PlayerCamera.Current.PointAtPos();
            var cellCoord = map.map.FromWorldPos(pos);
            transform.position = map.map.GetWorldPos(cellCoord.x, cellCoord.y);
            transform.localScale = map.map.CellSize;
        }
    }
#endif
}