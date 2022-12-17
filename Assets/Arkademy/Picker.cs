using System;
using System.Linq;
using UnityEngine;

namespace Arkademy
{
    public class Picker : MonoBehaviour
    {
        [SerializeField] private Detector detector;
        [SerializeField] private float range;

        private void FixedUpdate()
        {
            if (!detector) return;
            if (detector.Detected == null || detector.Detected.Count == 0) return;
            detector.Detected.Where(x=>x).Select(x =>x.GetComponentInParent<PickUp>())
                .Where(x => x && Vector2.Distance(transform.position, x.transform.position) <= range)
                .ToList().ForEach(x => x.PickBy(this));
        }
    }
}