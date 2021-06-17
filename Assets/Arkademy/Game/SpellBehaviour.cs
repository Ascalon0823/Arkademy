using System;
using UnityEngine;

namespace Arkademy.Game
{
    public enum SpellMediumType
    {
        Direct,
        Ray,
        Box,
        Projectile,
        Spray,
        Capsule
    }

    public class SpellBehaviour : MonoBehaviour
    {
        public SpellMediumType mediumType;
        public float minimumEnergy;
        public RayBehaviour rayPrefab;
        [SerializeField] private RayBehaviour currentRay;
        public BoxSpellBehaviour boxPrefab;
        [SerializeField] private BoxSpellBehaviour currentBox;
        public CapsuleSpellBehaviour capsulePrefab;
        [SerializeField] private CapsuleSpellBehaviour currCapsule;
        public ProjectileBehaviour projectilePrefab;
        public SpraySpellBehaviour sprayPrefab;
        [SerializeField] private SpraySpellBehaviour currSpray;


        public void HandleCastEvent(Caster.CastEvent castEvent)
        {
            switch (mediumType)
            {
                case SpellMediumType.Direct:
                    Debug.Log($"Cast on {castEvent.currTarget}");
                    break;
                case SpellMediumType.Ray:
                    HandleRaySpell(castEvent);
                    break;
                case SpellMediumType.Box:
                    HandleBoxSpell(castEvent);
                    break;
                case SpellMediumType.Projectile:
                    HandleProjectile(castEvent);
                    break;
                case SpellMediumType.Spray:
                    HandleSpray(castEvent);
                    break;
                case SpellMediumType.Capsule:
                    HandleCapsule(castEvent);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void HandleSpray(Caster.CastEvent castEvent)
        {
            if (currSpray == null)
            {
                currSpray = Instantiate(sprayPrefab);
                currSpray.Ignores.Add(castEvent.caster.GetComponentInChildren<Collider>());
            }
            var origin = castEvent.caster.transform.position;
            var displacement = castEvent.currPos -origin;
            displacement = Vector3.ProjectOnPlane(displacement, Vector3.up);
            var dir = displacement.normalized;
            currSpray.transform.position = origin;
            currSpray.transform.forward = dir;
            if (castEvent.state == Caster.CastState.End)
            {
                Destroy(currSpray.gameObject);
            }
        }
        private void HandleCapsule(Caster.CastEvent castEvent)
        {
            if (currCapsule == null)
            {
                currCapsule = Instantiate(capsulePrefab);
                currCapsule.Ignores.Add(castEvent.caster.GetComponentInChildren<Collider>());
                currCapsule.transform.position = castEvent.originPos;
            }
            if (castEvent.state == Caster.CastState.End)
            {
                Destroy(currCapsule.gameObject);
            }
        }
        private void HandleProjectile(Caster.CastEvent castEvent)
        {
            if (castEvent.state == Caster.CastState.End)
            {
                var origin = castEvent.caster.transform.position;
                var dir = castEvent.currPos - origin;
                dir = Vector3.ProjectOnPlane(dir, Vector3.up).normalized;
                var proj = Instantiate(projectilePrefab);
                proj.transform.position = origin;
                proj.velocity = dir * 10f;
                proj.Ignores.Add(castEvent.caster.GetComponentInChildren<Collider>());
                proj.remainingTime = 5f;
                proj.triggerRadius = 0.5f;
                proj.triggerCountBeforeKill = 2;
            }
        }

        private void HandleRaySpell(Caster.CastEvent castEvent)
        {
            var origin = castEvent.caster.transform.position;
            var dir = castEvent.currPos - origin;
            dir = Vector3.ProjectOnPlane(dir, Vector3.up).normalized;
            if (currentRay == null)
            {
                currentRay = Instantiate(rayPrefab);
                currentRay.Ignores.Add(castEvent.caster.GetComponentInChildren<Collider>());
            }
            currentRay.direction = dir;
            currentRay.origin = origin;
            if (castEvent.state == Caster.CastState.End)
            {
                Destroy(currentRay.gameObject);
            }
        }

        private void HandleBoxSpell(Caster.CastEvent castEvent)
        {
            if (currentBox == null)
            {
                currentBox = Instantiate(boxPrefab);
                currentBox.Ignores.Add(castEvent.caster.GetComponentInChildren<Collider>());
                currentBox.transform.position = castEvent.originPos;
                currentBox.transform.localScale = Vector3.one * 2.5f;
            }
            if (castEvent.state == Caster.CastState.End)
            {
                Destroy(currentBox.gameObject);
            }
        }
    }
}