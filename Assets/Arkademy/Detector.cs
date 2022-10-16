using System;
using System.Collections.Generic;
using UnityEngine;

namespace Arkademy
{
    public class Detector : MonoBehaviour
    {
        public List<Collider2D> Detected => detected;
        [SerializeField]private List<Collider2D> detected = new List<Collider2D>();
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (detected.Contains(other)) return;
            detected.Add(other);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (detected.Contains(other)) return;
            detected.Add(other);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!detected.Contains(other)) return;
            detected.Remove(other);
        }
    }
}