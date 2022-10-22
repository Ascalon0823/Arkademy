using System;
using UnityEngine;

namespace Arkademy.Graphics
{
    public class HighlightControl : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer target;
        [SerializeField] private Material highlighted;
        [SerializeField] private Material original;

        private void Awake()
        {
            original = target.material;
        }

        private void Start()
        {
            ToggleHighlight(false);
        }

        public void ToggleHighlight(bool active)
        {
            target.material = active ? highlighted : original;
        }
    }
}
