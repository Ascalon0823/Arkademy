using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Arkademy.Spells.SpellEffects
{
    public static class SpellEffectFactory
    {
        public static void Beam(Vector3 origin, Vector3 destination, float castedTime) {
            Debug.DrawLine(origin, destination, Color.red * castedTime/3f);
            Debug.Log("Beam effect");
        }

        public static void Projectile(Vector3 origin, Vector3 destination) {
            Debug.Log("Projectile effect");
        }
    }
}