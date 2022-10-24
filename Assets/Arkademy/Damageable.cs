using System;
using UnityEngine;

namespace Arkademy
{
    public class Damageable : MonoBehaviour
    {
        public int maxLife;
        public int currLife;

        public Action<int> AfterReceiveDamage;
        public Action<int, int> AfterLifeUpdated;
        public void TakeDamage(int damage)
        {
            currLife -= damage;
            AfterReceiveDamage?.Invoke(damage);
        }
    }
}
