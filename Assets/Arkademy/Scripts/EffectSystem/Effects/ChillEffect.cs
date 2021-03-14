using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arkademy.EffectSystem
{
    public class ChillEffect : Effect
    {
        public ChillEffect(float energy, Vector3 dir, Vector3 pos) : base(energy, dir, pos)
        {
        }
        public override void Apply(PropertiesHolder holder)
        {
            if (holder.ObjectGroup.TryGetProperty("Temperature", out var temp))
            {
                holder.ObjectGroup.Update("Temperature",temp - magnitude);
            }
        }


    }
}
