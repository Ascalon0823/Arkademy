using System;
using System.Linq;
using UnityEngine;

namespace Arkademy
{
    public class Interaction : MonoBehaviour
    {
        public Interaction currTarget;
        public Interaction currCandidate;
        public Detector usingDetector;

        public void InteractWith(Interaction other)
        {
            currTarget = other;
            other.currTarget = this;
        }
        
        public void InteractWithCurrentCandidate()
        {
            if (!currCandidate) return;
            InteractWith(currCandidate);
        }

        public void StopInteraction()
        {
            currTarget.currTarget = null;
            currTarget = null;
        }

        private void Update()
        {
            currCandidate = GetInteractionCandidate();
        }

        private Interaction GetInteractionCandidate()
        {
            if (!usingDetector) return null;
            if (usingDetector.Detected == null || usingDetector.Detected.Count == 0) return null;
            var validDetected = usingDetector.Detected
                .Where(x => x.gameObject.layer == LayerMask.NameToLayer("Default") && !x.isTrigger &&
                            x.GetComponentInParent<Interaction>())
                .Select(x => x.GetComponentInParent<Interaction>()).ToList();
            if (!validDetected.Any()) return null;
            var nearest =
                validDetected.OrderBy(x => Vector2.Distance(x.transform.position, usingDetector.transform.position))
                    .First();
            return nearest;
        }
    }
}