using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arkademy
{
    public class Interactable : MonoBehaviour
    {
        public float range = 0f;

        public virtual void Interact(GameObject actor)
        {
            Debug.Log($"Interacted by {actor.name}", gameObject);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position,range);
        }
    }
}
