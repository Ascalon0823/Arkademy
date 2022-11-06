using System;
using UnityEngine;

namespace Arkademy
{
    public class ProjectileBehaviour : MonoBehaviour
    {
        public float lifeTime;
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

        private void Start()
        {
            spawnAt = transform.position;
            targetDir = transform.up;
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
            var dir = transform.up;
            if (homing)
            {
                dir = (targetTransform.position - transform.position).normalized;
            }

            if (useTargetPoint)
            {
                dir = (targetPoint - transform.position).normalized;
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
    }
}