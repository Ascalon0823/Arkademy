using UnityEngine;

namespace Arkademy.AI
{
    public class AIFollowTarget : MonoBehaviour
    {
        [SerializeField] private AIMotorControl motorControl;
        public Transform target;

        // Update is called once per frame
        void Update()
        {
            if (!motorControl||!target) return;
            motorControl.targetLocation = target.position;
        }
    }
}
