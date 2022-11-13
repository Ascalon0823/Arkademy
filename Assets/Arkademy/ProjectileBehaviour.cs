using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Arkademy
{
    public class ProjectileBehaviour : MonoBehaviour
    {
        public float lifeTime;
        public GameObject[] ignores;
        public Vector3 spawnAt;
        public Vector3 targetDir;
        public bool useTargetPoint;
        public Vector3 targetPoint;
        public bool homing;
        public Transform targetTransform;
        public int maxPierce;
        public int remainingPierce;
        public int maxRicochet;
        public int remainingRicochet;
        public float maxHomingAngle;
        public float acceleration;
        public float deceleration;
        public float maxSpeed;
        public float currSpeed;
        public float remainLifeTime;
        public Vector3 currVelocity;
        public float scatterRange;

        private void Start()
        {
            spawnAt = transform.position;
            targetPoint = targetTransform.position;
            targetDir = (targetPoint - spawnAt).normalized;
            transform.up = targetDir;
            remainLifeTime = lifeTime;
            remainingPierce = maxPierce;
            remainingRicochet = maxRicochet;
            currVelocity = transform.up * currSpeed;
        }

        void Update()
        {
            if (ShouldDestroy())
            {
                Destroy(gameObject);
                return;
            }

            remainLifeTime -= Time.deltaTime;
            var dir = targetDir;
            var scatter = (Random.Range(0, 1f) > 0.8f ? Random.insideUnitCircle : Vector2.zero)*scatterRange;
            if (homing)
            {
                dir = (targetTransform.position+(Vector3)scatter - transform.position).normalized;
            }

            if (useTargetPoint)
            {
                dir = (targetPoint+(Vector3)scatter - transform.position).normalized;
            }

            var accel = Mathf.Sqrt(Vector3.Dot(currVelocity, dir) > 0 ? acceleration : deceleration);
            currVelocity = new Vector3(
                Mathf.Lerp(currVelocity.x, dir.x * maxSpeed, accel * Time.deltaTime),
                Mathf.Lerp(currVelocity.y, dir.y * maxSpeed, accel * Time.deltaTime), 0f);
            var rot = Quaternion.Slerp(Quaternion.LookRotation(Vector3.forward, transform.up),
                Quaternion.LookRotation(Vector3.forward, dir),
                maxHomingAngle * Time.deltaTime);
            transform.rotation = rot;
            transform.position += currVelocity * Time.deltaTime;
        }

        private bool ShouldDestroy()
        {
            return remainLifeTime <= 0 || (remainingPierce <= 0 && remainingPierce != maxPierce)
                                       || (remainingRicochet <= 0 && remainingRicochet != maxRicochet);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("Hit",other);
            if (ignores != null && ignores.Any(x=>other.transform.root.gameObject==x))
            {
                return;
            }
            var damageable = other.GetComponentInParent<Damageable>();

            if (damageable)
            {
                if (maxPierce != 0)
                {
                    remainingPierce--;
                }

                damageable.TakeDamage(10);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}