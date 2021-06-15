using System;
using System.Collections.Generic;
using UnityEngine;

namespace Arkademy.Game
{
    public class ProjectileBehaviour : MonoBehaviour
    {
        public float remainingTime;
        public Vector3 velocity;
        public Vector3 acceleration;
        public LayerMask triggerMask;
        public int triggerCountBeforeKill;
        public float triggerRadius;
        public readonly HashSet<Collider> Ignores = new HashSet<Collider>();
        private readonly HashSet<Collider> _triggerRecord = new HashSet<Collider>();
        public float timeScale;
        private void Start()
        {
            transform.localScale = Vector3.one * triggerRadius;
        }

        private void Update()
        {
            if (remainingTime <= 0)
            {
                Destroy(gameObject);
                return;
            }

            remainingTime -= Time.deltaTime * timeScale;
            transform.localScale = Vector3.one * triggerRadius;
            velocity += acceleration * (Time.deltaTime * timeScale);
            transform.position += velocity * (Time.deltaTime * timeScale);
            var colliders = Physics.OverlapSphere(transform.position, triggerRadius, triggerMask);
            foreach (var c in colliders)
            {
                if (_triggerRecord.Contains(c) || Ignores.Contains(c))
                {
                    continue;
                }

                Debug.Log($"Projectile {name} hit {c.name}");
                _triggerRecord.Add(c);
                if (triggerCountBeforeKill == -1 || _triggerRecord.Count <= triggerCountBeforeKill)
                {
                    continue;
                }

                Destroy(gameObject);
                return;
            }
        }
    }
}