using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Arkademy.Game
{
    public class RayBehaviour : MonoBehaviour
    {
        public Vector3 origin;
        public Vector3 direction;
        public float maxDistance;
        public LayerMask triggerMask;
        public LineRenderer lineRenderer;
        public readonly HashSet<Collider> Ignores = new HashSet<Collider>();
        public float width;
        public int pierceCount;

        private void Start()
        {
            lineRenderer.startWidth = width;
            lineRenderer.endWidth = width;
            lineRenderer.transform.position = origin;
            lineRenderer.SetPosition(0, Vector3.zero);
            lineRenderer.SetPosition(1, Vector3.zero);
        }

        private void Update()
        {
            var hits = Physics.SphereCastAll(new Ray(origin, direction), width / 2f, maxDistance, triggerMask);
            hits = hits.Where(x=>!Ignores.Contains(x.collider)).OrderBy(x => Vector3.Distance(x.point, origin)).ToArray();
            var destination = hits.Length>pierceCount
                ? hits[pierceCount].point
                : origin + direction.normalized * maxDistance;
            for (var i = 0; i <= Mathf.Min(pierceCount, hits.Length-1);i++)
            {
                Debug.Log($"Ray hits {hits[i].collider.name}");
            }
            lineRenderer.startWidth = width;
            lineRenderer.endWidth = width;
            lineRenderer.transform.position = origin;
            lineRenderer.SetPosition(0, Vector3.zero);
            lineRenderer.SetPosition(1, destination - origin);
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(origin + Vector3.Cross(Vector3.up, direction.normalized) * width / 2f,
                origin + direction.normalized * maxDistance);
            Gizmos.DrawLine(origin - Vector3.Cross(Vector3.up, direction.normalized) * width / 2f,
                origin + direction.normalized * maxDistance);
        }
    }
}