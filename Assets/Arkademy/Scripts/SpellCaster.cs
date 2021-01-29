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
        public Vector3 originDeltaPos;
        public Vector3 pointerPos;
        public Vector3 pointerDeltaPos;
    }
    public class SpellCaster : MonoBehaviour
    {
        
    }
}
