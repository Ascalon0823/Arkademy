using System;
using UnityEngine;

namespace Arkademy.Graphics
{
    public class DamageableControl : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer sprite;

        private Damageable damageable;
        private LTDescr descrMove;
        private LTDescr descrColor;

        private void Start()
        {
            damageable = GetComponentInParent<Damageable>();
            if (!damageable) return;
            damageable.AfterReceiveDamage += OnReceiveDamage;
        }

        private void OnDestroy()
        {
            damageable.AfterReceiveDamage -= OnReceiveDamage;
        }

        public void OnReceiveDamage(int damage)
        {
            
            descrColor = LeanTween.color(sprite.gameObject, Color.red, 0.05f).setOnComplete(_ =>
                LeanTween.color(sprite.gameObject, Color.white, 0.15f).setEaseOutCubic());
            descrMove = LeanTween.moveLocalX(sprite.gameObject, 0.1f, 0.04f).setEaseOutSine().setLoopPingPong(4).setOnComplete(x=>sprite.transform.localPosition = Vector3.zero);
        }
    }
}