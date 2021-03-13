using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Arkademy.EffectSystem;
using Arkademy.PropertySystem;

namespace Arkademy
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class PropertiesHolder : MonoBehaviour
    {
        public PropertyGroup ObjectGroup = new PropertyGroup()
        {
            groupTag = "PhysicalObject",
            properties = new List<Property>
            {
                new Property{key = "Temperature",value=0f},
                new Property{key = "Humidity",value = 0f},
                new Property{key = "Charge",value = 0f},
                new Property{key = "Strength",value = 1f}
            }
        };
        public static bool TryGetPropHolderFromCollider(Collider c, out PropertiesHolder holder)
        {
            holder = c.GetComponent<PropertiesHolder>();
            return holder != null;
        }
    
        void Update()
        {
        }

        public bool ReceiveEffect(Effect effect)
        {
            if (!CanApplyEffect(effect)) return false;
            effect.Apply(ref ObjectGroup);
            return true;
        }

        private bool CanApplyEffect(Effect effect)
        {
            return true;
        }
    }
}

