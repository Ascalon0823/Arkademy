using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Arkademy.Pawns;
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

        public virtual void Interact(Pawn actor)
        {
            Debug.Log($"{actor.name} interacts with", this);
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position,range);
        }
    }
}
