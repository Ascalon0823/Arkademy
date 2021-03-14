using System;
using System.Collections;
using System.Collections.Generic;
using Arkademy.PropertySystem;
using UnityEngine;

namespace Arkademy.EffectSystem
{
    public abstract class Effect
    {
        public enum Type
        {
            Heat,
            Chill,
            Kinect,
            Static,
            Accel,
        }
        public float magnitude;
        public Vector3 direction;
        public Vector3 position;

        public Effect(float energy, Vector3 dir, Vector3 pos)
        {
            magnitude = energy;
            direction = dir;
            position = pos;
        }
        public abstract void Apply(PropertiesHolder holder);
    }
    public static class EffectTypeExtension
    {
        public static Effect NewEffect(this Effect.Type type, float energy,Vector3 direction,Vector3 position)
        {
            switch (type)
            {
                case Effect.Type.Heat:
                    return new HeatEffect(energy,direction,position);
                    break;
                case Effect.Type.Chill:
                    return new ChillEffect(energy,direction,position);
                    break;
                case Effect.Type.Kinect:
                    return new KinectEffect(energy,direction,position);
                case Effect.Type.Static:
                    return new StaticEffect(energy,direction,position);
                case Effect.Type.Accel:
                    return new AccelEffect(energy,direction,position);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}

