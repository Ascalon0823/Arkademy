using System.Collections;
using System.Collections.Generic;
using Arkademy.PropertySystem;
using UnityEngine;

namespace Arkademy.EffectSystem
{
    public class HeatEffect : Effect
    {
        public HeatEffect(float energy, Vector3 dir, Vector3 pos) : base(energy, dir, pos)
        {
        }

        public override void Apply(PropertiesHolder holder)
        {
            if (holder.ObjectGroup.TryGetProperty("Temperature", out var temp))
            {
                holder.ObjectGroup.Update("Temperature",temp + magnitude);
            }
            if (holder.ObjectGroup.TryGetProperty("Humidity", out temp))
            {
                holder.ObjectGroup.Update("Humidity",temp - magnitude);
            }
        }
    }
}