using UnityEngine;

namespace Arkademy.Graphics
{
    public class ActorFacingControl : MonoBehaviour
    {
        public Facing facing;
        public bool originallyFacingRight;
        public SpriteRenderer sr;
        // Update is called once per frame
        private void Update()
        {
            if (!facing || !sr) return;
            if (facing.facingDir.magnitude < float.Epsilon) return;
            sr.flipX = Vector2.Dot(facing.facingDir, Vector2.right) > 0 ? !originallyFacingRight : originallyFacingRight;
        }
    }
}
