using System;
using UnityEngine;
using UnityEngine.Events;

namespace Arkademy
{
    public class Destructible : MonoBehaviour
    {
        public int maxDurability;

        public int currDurability;

        public Action<Destructible> AfterDestruction;
        public UnityEvent<Destructible> AfterDestructionEvent;

        [SerializeField] private Damageable damageable;

        private void Start()
        {
            damageable = GetComponentInChildren<Damageable>();
            if (!damageable) return;

            damageable.AfterReceiveDamage += HandleDamage;
        }

        private void OnDestroy()
        {
            AfterDestructionEvent = null;
            if (!damageable) return;
            damageable.AfterReceiveDamage -= HandleDamage;
        }

        private void Destruction()
        {
            AfterDestructionEvent?.Invoke(this);
            AfterDestruction?.Invoke(this);
        }

        private void HandleDamage(int damage)
        {
            if (currDurability <= 0) return;
            currDurability -= damage;
            if (currDurability <= 0)
            {
                Destruction();
            }
        }
    }
}