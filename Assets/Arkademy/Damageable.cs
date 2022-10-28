using System;
using UnityEngine;

namespace Arkademy
{
    public class Damageable : MonoBehaviour
    {
        public Action<int> AfterReceiveDamage;
        public void TakeDamage(int damage)
        {
            if (!enabled) return;
            AfterReceiveDamage?.Invoke(damage);
        }
    }
}
