using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arkademy.EffectSystem
{
    public class ChillEffect : Effect
    {
        public ChillEffect(float energy)
        {
            type = Type.Chill;
            magnitude = energy;
        }
    }
}
