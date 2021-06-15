using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace Arkademy.Game
{
    public class BoxSpellBehaviour : MonoBehaviour
    {
        public LayerMask triggerMask;
        public readonly HashSet<Collider> Ignores = new HashSet<Collider>();
        private void Update()
        {
            var t = transform;
            foreach (var c in Physics.OverlapBox(t.position, t.localScale/2f,Quaternion.identity,triggerMask)
                .Where(x=>!Ignores.Contains(x)))
            {
                Debug.Log($"Ray hits {c.name}");
            }
        }
    }
}