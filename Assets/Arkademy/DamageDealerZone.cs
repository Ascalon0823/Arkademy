using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Arkademy
{
    public class DamageDealerZone : MonoBehaviour
    {
        public int damage;
        public float interval;
        private readonly Dictionary<Damageable, int> damageMemo = new Dictionary<Damageable, int>();

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.isTrigger) return;
            var damageable = other.GetComponentInParent<Damageable>();
            if (!damageable) return;
            if (damageMemo.ContainsKey(damageable)) return;
            damageable.TakeDamage(damage);
            damageMemo[damageable] = Time.frameCount;
        }
        

        private void Update()
        {
            var damageables = damageMemo.Keys.ToList();

            foreach (var damageable in damageables)
            {
                if (Time.frameCount - damageMemo[damageable] < interval / Time.deltaTime) return;
                damageable.TakeDamage(damage);
                damageMemo[damageable] = Time.frameCount;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.isTrigger) return;
            var damageable = other.GetComponentInParent<Damageable>();
            if (!damageable || !damageMemo.ContainsKey(damageable)) return;
            damageMemo.Remove(damageable);
        }
    }
}