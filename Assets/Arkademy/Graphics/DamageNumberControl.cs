using System;
using UnityEngine;

namespace Arkademy.Graphics
{
    public class DamageNumberControl : MonoBehaviour
    {
        [SerializeField] private Transform damageNumberRoot;
        private Damageable damageable;
        [SerializeField] private DamageNumberGroup groupPrefab;

        private void Start()
        {
            damageable = GetComponentInParent<Damageable>();
            if (!damageable) return;
            damageable.AfterReceiveDamage += ReceiveDamageHandler;
        }

        private void OnDestroy()
        {
            damageable.AfterReceiveDamage -= ReceiveDamageHandler;
        }

        private void ReceiveDamageHandler(int damage)
        {
            var group = Instantiate(groupPrefab);
            group.transform.position = damageNumberRoot.position;
            group.Setup(new[] {damage, damage, damage}, damageNumberRoot);
        }
    }
}