using UnityEngine;

namespace Arkademy.AI
{
    public class AIMotorControl : MonoBehaviour
    {
        [SerializeField] private Motor motor;
        [SerializeField] private float stopDistance;
        public Vector3 targetLocation;


        public void SetTarget(Vector3 target)
        {
            targetLocation = target;
        }
        // Update is called once per frame
        private void Update()
        {
            if (!motor) return;
            var dir = targetLocation - transform.position;
            if (dir.magnitude < stopDistance)
            {
                motor.moveDir=Vector2.zero;
                return;
            }
            motor.moveDir = dir.normalized;
        }
    }
}
