using UnityEngine;

namespace Arkademy
{
    public class Interaction : MonoBehaviour
    {
        public Interaction currTarget;

        public void InteractWith(Interaction other)
        {
            currTarget = other;
            other.currTarget = this;
        }

        public void StopInteraction()
        {
            currTarget.currTarget = null;
            currTarget = null;
        }
    }
}