using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arkademy
{
    public class MapRenderer : MonoBehaviour
    {
        [SerializeField] private Map map;
        [SerializeField] private MeshFilter filter;
        [SerializeField] private MeshRenderer renderer;
        [SerializeField] private MeshCollider collider;
        private Mesh _mesh;

        private void Start()
        {
            if (map == null) map = GetComponentInParent<Map>();
            map.onMapBuilt += RebuildRenderer;
        }

        private void RebuildRenderer()
        {
            if (filter == null) filter = gameObject.AddComponent<MeshFilter>();
            if (renderer == null) renderer = gameObject.AddComponent<MeshRenderer>();
            if (collider == null) collider = gameObject.AddComponent<MeshCollider>();
            _mesh = new Mesh();
            filter.sharedMesh = _mesh;
            collider.sharedMesh = null;
            collider.sharedMesh = _mesh;
        }

        private void OnDrawGizmosSelected()
        {
            if (map == null) return;
            for (var i = 0; i < map.Width; i++)
            for (var j = 0; j < map.Height; j++)
            {
                Gizmos.DrawWireCube(map.GetCenterOfCell(i, j), Map.CellSize * Vector3.one);
            }
        }
    }
}