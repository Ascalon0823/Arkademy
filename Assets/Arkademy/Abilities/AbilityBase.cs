using System;
using UnityEngine;

namespace Arkademy.Abilities
{
    public abstract class AbilityBase : MonoBehaviour
    {
        public float reuseTime;
        public float remainingReuseTime;

        public virtual bool TryUse()
        {
            if (!CanUse()) return false;
            PayLoad();
            return true;
        }

        public virtual bool CanUse()
        {
            return remainingReuseTime <= 0f;
        }

        public abstract void PayLoad();

        protected virtual void Update()
        {
            if (remainingReuseTime <= 0) return;
            remainingReuseTime -= Time.deltaTime;
            remainingReuseTime = Mathf.Max(0, remainingReuseTime);
        }
    }
}
