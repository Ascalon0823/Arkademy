using System;
using UnityEngine;

// ReSharper disable once IdentifierTypo
namespace Arkademy
{
    [Serializable]
    public class Spell
    {


        public string name;
        public int minEnergy;
        public int maxEnergy;

        private Spell()
        {
            name = "default spell";
            minEnergy = 0;
            maxEnergy = 0;
        }

        public static Spell New()
        {
            return new Spell();
        }
        public void OnDown(Caster caster, Vector3 point)
        {
        }

        public void OnHeld(Caster caster, Vector3 point)
        {
        }

        public void OnUp(Caster caster, Vector3 point)
        {
        }
    }
}