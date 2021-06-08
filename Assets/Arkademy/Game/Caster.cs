using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arkademy.Game{
    public class Caster : MonoBehaviour
    {
        public enum CastState
        {
            Begin,
            Casting,
            End,
            Cancel
        }
        [Serializable]
        public struct CastEvent
        {
            public Caster caster;
            public Collider currTarget;
            public Collider originTarget;
            public Vector3 originPos;
            public CastState state;
            public Vector3 currPos;
            public float timePassed;
        }

        public SpellBehaviour loadedSpell;

        public void HandleCastEvent(CastEvent castEvent)
        {
            if (loadedSpell == null)
            {
                return;
            }
            switch (loadedSpell.targetType)
            {
                case SpellTargetType.Objects:
                    Debug.Log($"Cast on {castEvent.originTarget}");
                    break;
                case SpellTargetType.Direction:
                    var casterPos = castEvent.caster.transform.position;
                    Debug.DrawRay(casterPos,castEvent.currPos-casterPos);
                    break;
                case SpellTargetType.Area:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
    }

}

