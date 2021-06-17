using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arkademy.Game
{
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
        public CastEvent lastCastEvent;
        public float energy;
        public float maxEnergy;
        public float energyRegen;
        public float timeScale;
        public float enegryOutputRate;
        public void HandleCastEvent(CastEvent castEvent)
        {
            if (loadedSpell == null)
            {
                return;
            }

            loadedSpell.HandleCastEvent(castEvent);
            lastCastEvent = castEvent;
        }

        private void Update()
        {
            if (energy < maxEnergy)
            {
                energy = Mathf.Min(energy + energyRegen * Time.deltaTime * timeScale, maxEnergy);
            }
        }
    }
}