using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Arkademy
{
    public class DamageDealer : MonoBehaviour
    {
        public int faction;
        public int amount;
        public float interval;
        private readonly Dictionary<Damageable,float> damageMemo = new Dictionary<Damageable,float>();
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.isTrigger) return;
            var damageable = other.GetComponentInParent<Damageable>();
            if (!damageable||damageable.faction == faction) return;
            if (damageMemo.ContainsKey(damageable)) return;
            damageMemo[damageable] = Time.timeSinceLevelLoad;
            damageable.TakeDamage(amount);
        }

        private void Update()
        {
            var damageables = damageMemo.Keys.ToList();

            foreach (var damageable in damageables)
            {
                if (interval <= 0) continue;
                if (Time.timeSinceLevelLoad - damageMemo[damageable] < interval) continue;
                damageable.TakeDamage(amount);
                damageMemo[damageable] = Time.timeSinceLevelLoad;
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
