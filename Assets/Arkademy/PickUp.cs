using System;
using UnityEngine;

namespace Arkademy
{
    public class PickUp : MonoBehaviour
    {
        public float dropTime;
        [SerializeField] private Collider2D collider;

        private void Awake()
        {
            collider.enabled = false;
        }

        private void Start()
        {
            CancelInvoke();
            Invoke(nameof(EnableCollider), dropTime);
        }

        private void EnableCollider()
        {
            collider.enabled = true;
        }

        public void PickBy(Picker picker)
        {
            Destroy(gameObject);
        }
    }
}