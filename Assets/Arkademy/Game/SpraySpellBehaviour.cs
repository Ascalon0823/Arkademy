using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Arkademy.Game
{
    
    public class SpraySpellBehaviour : MonoBehaviour
    {
        public int angle;
        public int resolution;
        public float radius;
        public LayerMask triggerMask;
        public readonly HashSet<Collider> Ignores = new HashSet<Collider>();
        [SerializeField] private FanMesh mesh;

        private void Start()
        {
            Update();
        }

        private void Update()
        {
            if (!mesh)
            {
                return;
            }
            mesh.UpdateMesh(angle,radius,resolution);
            var origin = transform.position;
            var f = transform.forward;
            var colliders = new HashSet<Collider>();
            for (var i = 0; i <= angle / resolution; i++)
            {
                var curr = i * resolution - angle / 2f;
                var dir = Quaternion.Euler(0f, curr, 0f) * f.normalized ;
                foreach (var c in Physics.RaycastAll(origin, dir, radius, triggerMask).Where(x=>!Ignores.Contains(x.collider)))
                {
                    colliders.Add(c.collider);
                }
            }

            foreach (var c in colliders)
            {
                Debug.Log($"Spray hit {c.name}");
            }
        }

        private void OnDrawGizmos()
        {
            var origin = transform.position;
            var f = transform.forward;
            for (var i = 0; i <= angle / resolution; i++)
            {
                var curr = i * resolution - angle / 2f;
                var dest = origin + Quaternion.Euler(0f, curr, 0f) * f.normalized * radius;
                Gizmos.DrawLine(origin,dest);
            }
        }
    }
}