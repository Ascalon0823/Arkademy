using System.Collections;
using System.Collections.Generic;
using Arkademy.PropertySystem;
using UnityEngine;

namespace Arkademy.EffectSystem
{
    public class HeatEffect : Effect
    {
        public HeatEffect(float energy)
        {
            type = Type.Heat;
            magnitude = energy;
        }

        public override void Apply(ref PropertyGroup propertyGroup)
        {
            if (propertyGroup.TryGetProperty("Temperature", out var temp))
            {
                propertyGroup.Update("Temperature",temp + magnitude);
            }
            if (propertyGroup.TryGetProperty("Humidity", out temp))
            {
                propertyGroup.Update("Humidity",temp - magnitude);
            }
        }
    }
}