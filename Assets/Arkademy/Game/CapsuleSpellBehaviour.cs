using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace Arkademy
{
    public class CapsuleSpellBehaviour : MonoBehaviour
    {
        public LayerMask triggerMask;
        public float radius;
        public float height;
        public readonly HashSet<Collider> Ignores = new HashSet<Collider>();

        private void Start()
        {
            Update();
        }

        private void Update()
        {
            transform.localScale = new Vector3(radius * 2f, height , radius * 2f);
            var pos = transform.position;
            foreach (var c in Physics.OverlapCapsule(pos+Vector3.up*height, pos-Vector3.up*height,radius,triggerMask)
                .Where(x=>!Ignores.Contains(x)))
            {
                Debug.Log($"Capsule hits {c.name}");
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position,radius);
            Gizmos.DrawWireSphere(transform.position+Vector3.up*height,radius);
            Gizmos.DrawWireSphere(transform.position-Vector3.up*height,radius);
        }
    }
}