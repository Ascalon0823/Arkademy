using UnityEngine;

namespace Arkademy
{
    public class Energy : MonoBehaviour
    {
        public float max;

        public float curr;

        public float regen;

        public void Consume(float amount)
        {
            curr = Mathf.Max(curr - amount, 0f);
        }
        private void Update()
        {
            if (curr >= max) return;
            curr += regen * Time.deltaTime;
        }
    }
}
