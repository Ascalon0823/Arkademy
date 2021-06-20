using System;
using System.Collections.Generic;
using UnityEngine;

namespace Arkademy.Game
{
    [RequireComponent(typeof(MeshFilter))]
    public class FanMesh : MonoBehaviour
    {
        [SerializeField] private int currAngle;
        [SerializeField] private float currRadius;
        [SerializeField] private int currReso;
        [SerializeField] private MeshFilter mf;
        [SerializeField] private Mesh m;
        [SerializeField] private bool buildOnStart;
        [SerializeField] private bool blockByScene;
        [SerializeField] private LayerMask blockBy;
        private void Start()
        {
            if (!buildOnStart)
            {
                return;
            }
            BuildMesh(currAngle,currRadius,currReso);
        }

        private void Update()
        {
            if (!blockByScene)
            {
                return;
            }
            BuildMesh(currAngle,currRadius,currReso);
        }

        public void UpdateMesh(int angle, float radius, int resolution)
        {
            if (angle == currAngle && radius.Equals(currRadius) && resolution == currReso)
            {
                return;
            }

            BuildMesh(angle, radius, resolution);
        }

        private void BuildMesh(int angle, float radius, int resolution)
        {
            if (m == null)
            {
                m = new Mesh();
                mf.sharedMesh = m;
            }

            var verts = new List<Vector3>();
            verts.Add(Vector3.zero);
            var normal = new List<Vector3>();
            normal.Add(Vector3.up);
            var tri = new List<int>();
            for (var i = 0; i <= angle / resolution; i++)
            {
                var curr = i * resolution - angle / 2f;
                var dest = Quaternion.Euler(0f, curr, 0f) * Vector3.forward * radius;
                if (Physics.Raycast(transform.position, transform.rotation*dest.normalized, out var hit, radius, blockBy))
                {
                    dest =  hit.point - transform.position;
                }
                verts.Add(dest);
                normal.Add(Vector3.up);
                if (i == 0)
                {
                    continue;
                }

                tri.AddRange(new[]
                {
                    0, verts.Count - 2, verts.Count - 1
                });
            }

            m.Clear();

            m.SetVertices(verts);
            m.SetTriangles(tri, 0);
            m.SetNormals(normal);
        }
    }
}