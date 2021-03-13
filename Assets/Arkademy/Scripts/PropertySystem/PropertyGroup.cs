using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Arkademy.PropertySystem
{
    [System.Serializable]
    public struct PropertyGroup
    {
        public string groupTag;
        public List<Property> properties;

        public bool TryGetProperty(string key,out float f)
        {
            f = 0f;
            var idx = properties.FindIndex(x => key == x.key);
            if (idx == -1) return false;
            f = properties[idx].value;
            return true;
        }
        public bool Update(string key, float newValue)
        {
            var idx = properties.FindIndex(x => key == x.key);
            if (idx==-1) return false;
            properties[idx] = new Property {key = key, value = newValue};
            return true;
        }
    }
}
