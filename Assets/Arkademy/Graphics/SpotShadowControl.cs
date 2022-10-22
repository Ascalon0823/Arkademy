using System;
using UnityEngine;

namespace Arkademy.Graphics
{
    public class SpotShadowControl : MonoBehaviour
    {
        [SerializeField] private Transform shadow;
        [SerializeField] private Transform target;
        [SerializeField] private float maxDistance;
        [SerializeField] private float minDistance;
        [SerializeField] private Vector3 minScale;
        [SerializeField] private int steps;
        void Update()
        {
            var t =
                Mathf.Clamp01(Mathf.Abs(target.position.y - shadow.position.y) / (maxDistance - minDistance));
            shadow.localScale = Vector3.Lerp(Vector3.one, minScale, Mathf.RoundToInt(t * steps) * 1.0f / steps);
        }
    }
}