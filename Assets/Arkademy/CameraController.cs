using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Arkademy
{
    [RequireComponent(typeof(Camera))]
    public class CameraController : MonoBehaviour
    {
        private static CameraController _instance;
        [SerializeField] private Transform followTarget = null;
        [SerializeField] private Vector3 offset = Vector3.zero;
        [SerializeField] private float distance = 0f;
        [SerializeField] private uint smoothSpeed = 0;
        [SerializeField] private Camera cam;
        private void Awake()
        {
            _instance = this;
        }

        private void Start()
        {
            cam = GetComponent<Camera>();
        }

        private void Update()
        {
            if (null == followTarget) return;
            var trans = transform;
            var position = trans.position;
            var oldPos = position;
            position = followTarget.position + offset.normalized * distance;
            trans.forward = -offset.normalized;
            trans.position = Vector3.Slerp(oldPos, position, Time.deltaTime * (1 + smoothSpeed));
        }

        public static Camera GetCamera()
        {
            return _instance.cam;
        }

        public static Ray GetRay(Vector3? screenPoint = null)
        {
            return GetCamera().ScreenPointToRay(screenPoint ?? Input.mousePosition);
        }
    }
}