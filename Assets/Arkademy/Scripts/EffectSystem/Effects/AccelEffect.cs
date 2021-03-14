using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arkademy.EffectSystem
{
    public class AccelEffect : Effect
    {
        public AccelEffect(float energy, Vector3 dir, Vector3 pos) : base(energy, dir, pos)
        {
        }
        public override void Apply(PropertiesHolder holder)
        {
            var rb = holder.GetComponent<Rigidbody>();
            if (rb == null) return;
            var velocity = rb.velocity;
            velocity = Mathf.Max(velocity.magnitude + magnitude,0f)*velocity.normalized;
            rb.velocity = velocity;
        }


    }

}
