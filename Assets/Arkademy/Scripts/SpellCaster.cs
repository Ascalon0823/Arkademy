using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arkademy
{
    public struct CastEventData
    {
        public SpellCaster caster;
        public bool castBegin;
        public bool casting;
        public bool castEnd;
        public float castedTime;
        public Vector3 castOrigin;
        public Vector3 deltaPos;
        public Vector3 currentPos;
    }
    public class SpellCaster : MonoBehaviour
    {
        
    }
}
