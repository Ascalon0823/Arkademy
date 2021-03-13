using System;
using System.Collections;
using System.Collections.Generic;
using Arkademy.PropertySystem;
using UnityEngine;

namespace Arkademy.EffectSystem
{
    [System.Serializable]
    public class Effect
    {
        public enum Type
        {
            Heat,
            Chill
        }

        public Type GetType => type;
        [SerializeField]protected Type type;
        public float magnitude;

        public virtual void Apply(ref PropertyGroup propGroup)
        {
            
        }
    }
    public static class EffectTypeExtension
    {
        public static Effect NewEffect(this Effect.Type type, float energy)
        {
            switch (type)
            {
                case Effect.Type.Heat:
                    return new HeatEffect(energy);
                    break;
                case Effect.Type.Chill:
                    return new ChillEffect(energy);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}

