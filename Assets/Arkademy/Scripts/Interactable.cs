using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arkademy
{
    [RequireComponent(typeof(SphereCollider))]
    public class Interactable : MonoBehaviour
    {
        public float range = 0f;
        [SerializeField] private SphereCollider triggerSphere;
        protected void Awake()
        {
            triggerSphere = GetComponent<SphereCollider>();
            triggerSphere.isTrigger = true;
            triggerSphere.radius = range;
        }

        public virtual void Interact(GameObject actor)
        {
            Debug.Log($"Interacted by {actor.name}", gameObject);
        }

        public virtual void Highlight(GameObject actor,bool active)
        {
            Debug.Log($"Highlighted by {actor.name} : {active}", gameObject);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position,range);
        }
    }
}
