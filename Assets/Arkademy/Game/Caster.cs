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
            End,
            Begin,
            Casting,
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
        public float regenRecoverTime;
        [SerializeField]private float regenUnblockTimer;
        public void HandleCastEvent(CastEvent castEvent)
        {
            if (loadedSpell == null)
            {
                return;
            }

            if (energy <= loadedSpell.minimumEnergy)
            {
                castEvent.state = CastState.Cancel;
            }
            loadedSpell.HandleCastEvent(castEvent);
            lastCastEvent = castEvent;
        }

        public void ConsumeEnergy(float amount)
        {
            energy = Mathf.Max(0f, energy - amount);
            regenUnblockTimer = regenRecoverTime;
        }

        private void Update()
        {
            if (regenUnblockTimer >= 0)
            {
                regenUnblockTimer -= regenRecoverTime * timeScale * Time.deltaTime;
                return;
            }
            if (energy < maxEnergy)
            {
                energy = Mathf.Min(energy + energyRegen * Time.deltaTime * timeScale, maxEnergy);
            }
        }
    }
}