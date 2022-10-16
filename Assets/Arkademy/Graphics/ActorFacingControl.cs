using UnityEngine;

namespace Arkademy.Graphics
{
    public class ActorFacingControl : MonoBehaviour
    {
        public Motor motor;
        public bool originallyFacingRight;
        public SpriteRenderer sr;
        // Update is called once per frame
        private void Update()
        {
            if (!motor || !sr) return;
            if (motor.moveDir.magnitude < float.Epsilon) return;
            sr.flipX = Vector2.Dot(motor.moveDir, Vector2.right) > 0 ? !originallyFacingRight : originallyFacingRight;
        }
    }
}
