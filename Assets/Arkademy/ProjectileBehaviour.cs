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
        public MonoBehaviour target;
        public float maxHomingAngle;
        public float acceleration;
        public float deceleration;
        public float maxSpeed;
        public float currSpeed;
        public float remainLifeTime;
        public Vector3 currVelocity;
        public float scatterRange;
        public LayerMask blockBy;

        private void Start()
        {
            spawnAt = transform.position;
            if (target&&target.enabled)
            {
                targetPoint = target.transform.position;
            }

            transform.up = targetDir;
            remainLifeTime = lifeTime;
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
            var scatter = (Random.Range(0, 1f) > 0.8f ? Random.insideUnitCircle : Vector2.zero) * scatterRange;
            if (homing&&target&&target.enabled)
            {
                targetDir = target.transform
                    ? (target.transform.position + (Vector3) scatter - transform.position).normalized
                    : targetDir;
            }

            if (useTargetPoint)
            {
                targetDir = (targetPoint + (Vector3) scatter - transform.position).normalized;
            }

            var accel = (!homing||Mathf.Sqrt(Vector3.Dot(currVelocity, targetDir)) > 0) ? acceleration : deceleration;
            currVelocity = new Vector3(
                Mathf.Lerp(currVelocity.x, targetDir.x * maxSpeed, accel * Time.deltaTime),
                Mathf.Lerp(currVelocity.y, targetDir.y * maxSpeed, accel * Time.deltaTime), 0f);
            var rot = Quaternion.Slerp(Quaternion.LookRotation(Vector3.forward, transform.up),
                Quaternion.LookRotation(Vector3.forward, targetDir),
                maxHomingAngle * Time.deltaTime);
            transform.rotation = rot;
            transform.position += currVelocity * Time.deltaTime;
        }

        private bool ShouldDestroy()
        {
            return remainLifeTime <= 0;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.isTrigger) return;
            if (ignores!=null&&ignores.ToList().Contains(other.transform.root.gameObject)) return;
            if ((blockBy & (1 << other.gameObject.layer)) == 1 << other.gameObject.layer)
            {
                Destroy(gameObject);
            }
        }
    }
}