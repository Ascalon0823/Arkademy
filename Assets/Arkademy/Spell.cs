using System;
using UnityEngine;

namespace Arkademy
{
    [Serializable]
    public class Spell
    {
        public enum Mechanism
        {
            Default,
            Charge,
            Channel,
            Toggle
        }

        public string name;
        public int minEnergy;
        public int maxEnergy;
        public Mechanism mechanism;

        private Spell()
        {
            name = "default spell";
            minEnergy = 0;
            maxEnergy = 0;
            mechanism = Mechanism.Default;
        }

        public static Spell New()
        {
            return new Spell();
        }
        public void OnCastBegin(Caster caster, Vector3 point)
        {
            switch (mechanism)
            {
                case Mechanism.Default:
                    Debug.Log($"{caster.name} casts {name} at {point}");
                    break;
                case Mechanism.Charge:
                    break;
                case Mechanism.Channel:
                    break;
                case Mechanism.Toggle:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void OnCasting(Caster caster, Vector3 point)
        {
            switch (mechanism)
            {
                case Mechanism.Default:
                    break;
                case Mechanism.Charge:
                    break;
                case Mechanism.Channel:
                    Debug.Log($"{caster.name} casts {name} at {point}");
                    break;
                case Mechanism.Toggle:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }

        public void OnCastEnd(Caster caster, Vector3 point)
        {
            switch (mechanism)
            {
                case Mechanism.Default:
                    break;
                case Mechanism.Charge:
                    
                    Debug.Log($"{caster.name} casts {name} at {point}");
                    break;
                case Mechanism.Channel:
                    break;
                case Mechanism.Toggle:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}