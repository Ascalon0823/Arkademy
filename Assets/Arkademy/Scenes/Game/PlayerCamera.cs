using System;
using UnityEngine;

namespace Arkademy.Scenes.Game
{
    [RequireComponent(typeof(Camera))]
    public class PlayerCamera : MonoBehaviour
    {
        public static PlayerCamera Current;
        [SerializeField] private Camera useCamera;
        [Header("Follow Setup")]
        [SerializeField] private Transform followTarget;
        [SerializeField] private Vector3 followOffset;
        [SerializeField] private float followDistance;

        public Ray GetRay(Vector3? screenPos = null)
        {
            return useCamera.ScreenPointToRay(screenPos ?? Input.mousePosition);
        }
        public Vector3 PointAt(Vector3? screenPos = null)
        {
            var zeroPlane = new Plane(Vector3.up, Vector3.zero);
            var pointingRay = GetRay(screenPos);
            zeroPlane.Raycast(pointingRay, out var enter);
            return pointingRay.GetPoint(enter);
        }
        private void Awake()
        {
            if (Current != this && Current != null)
            {
                Destroy(gameObject);
                return;
            }
            Current = this;
            if (useCamera != null)
            {
                return;
            }
            useCamera = GetComponent<Camera>();
        }

        private void LateUpdate()
        {
            Follow();
        }

        private void Follow()
        {
            if (null == followTarget)
            {
                return;
            }
            transform.position = followTarget.position + followOffset.normalized * followDistance;
            transform.LookAt(followTarget);
        }
    }
}
