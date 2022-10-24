using System;
using UnityEngine;

namespace Arkademy
{
    public class Damageable : MonoBehaviour
    {
        public Action<int> AfterReceiveDamage;
        public void TakeDamage(int damage)
        {
            AfterReceiveDamage?.Invoke(damage);
        }
    }
}
