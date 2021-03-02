using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Arkademy.Spells;
namespace Arkademy
{
    public enum CastStatus{
        
        Idle,
        Begin,
        Hold,
        End,
    }
    public struct CastEventData
    {
        public SpellCaster Caster;
        public CastStatus Status;
        public float CastedTime;
        public Vector3 CastOrigin;
        public Vector3 OriginDeltaPos;
        public Vector3 PointerPos;
        public Vector3 PointerDeltaPos;
    }
    public class SpellCaster : MonoBehaviour
    {
        [SerializeField] private ISpell loadedSpell = null;
        [SerializeField] private float castedTime;

        private void Awake()
        {
            loadedSpell = new IceBeam();
        }

        private void Update()
        {
            if (loadedSpell == null) return;
            var castEvent = new CastEventData
            {
                Caster = this,
                Status =  CastStatus.Idle
            };
            if (Input.GetMouseButtonDown(0))
            {
                castedTime = 0; // reset casted time
                castEvent.Status = CastStatus.Begin;
            } else if (Input.GetMouseButton(0))
            {
                castedTime += Time.deltaTime;
                castEvent.Status = CastStatus.Hold;
                castEvent.CastedTime = castedTime;
            } else if (Input.GetMouseButtonUp(0))
            {
                castEvent.Status = CastStatus.End;
            }
            loadedSpell.Cast(castEvent);
        }
    }
}
