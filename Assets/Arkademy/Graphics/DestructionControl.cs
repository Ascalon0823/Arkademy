using System;
using UnityEngine;

namespace Arkademy.Graphics
{
    public class DestructionControl : MonoBehaviour
    {
        [SerializeField] private Destructible destructible;
        [SerializeField] private SpriteRenderer sprite;

        private void Start()
        {
            destructible = GetComponentInParent<Destructible>();
            if (!destructible) return;
            destructible.AfterDestruction += HandleDestruction;
        }

        private void OnDestroy()
        {
            if (!destructible) return;
            destructible.AfterDestruction -= HandleDestruction;
        }

        private void HandleDestruction(Destructible des)
        {
            if (sprite)
            {
                LeanTween.rotateZ(sprite.gameObject, sprite.flipY ? -90 : 90, 1f).setEaseInQuad().setEaseOutBounce()
                    .setOnComplete(() => { Destroy(des.gameObject); });
            }
        }
    }
}