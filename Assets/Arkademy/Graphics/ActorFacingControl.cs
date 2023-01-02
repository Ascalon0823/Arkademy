using System;
using System.Collections.Generic;
using UnityEngine;

namespace Arkademy.Graphics
{
    public class ActorFacingControl : MonoBehaviour
    {
        [Serializable]
        public struct SpriteRendererSetup
        {
            public bool originallyFacingRight;
            public bool swapX;
            public bool swapY;
            public SpriteRenderer sr;
        }

        public Facing facing;

        public List<SpriteRendererSetup> setups;

        // Update is called once per frame
        private void Update()
        {
            if (!facing || setups == null) return;
            if (facing.facingDir.magnitude < float.Epsilon) return;
            foreach (var setup in setups)
            {
                if (!setup.sr) continue;
                if (setup.swapX)
                {
                    setup.sr.flipX = Vector2.Dot(facing.facingDir, Vector2.right) > 0
                        ? !setup.originallyFacingRight
                        : setup.originallyFacingRight;
                }

                if (setup.swapY)
                {
                    setup.sr.flipY = Vector2.Dot(facing.facingDir, Vector2.right) > 0
                        ? !setup.originallyFacingRight
                        : setup.originallyFacingRight;
                }
            }
        }
    }
}