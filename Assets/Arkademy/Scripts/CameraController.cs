using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cinemachine.Utility;
using UnityEngine;
using UnityEngine.Serialization;

namespace Arkademy
{
    [RequireComponent(typeof(Camera))]
    public class CameraController : MonoBehaviour
    {
        private static CameraController _instance;
        private static readonly Vector3 CenterOfScreen = new Vector3(Screen.width / 2f, Screen.height / 2f);
        private static readonly Vector3 CenteredMousePosNormalizer = new Vector3(2f / Screen.width, 2f / Screen.height);
        private static Vector3 centeredMousePos => Input.mousePosition - CenterOfScreen;
   
        private static Vector3 normalizedCenteredMousePos =>
            Vector3.Scale(centeredMousePos, CenteredMousePosNormalizer);

        [SerializeField] private Transform followTarget = null;
        [SerializeField] private Vector3 offset = Vector3.zero;
        [SerializeField] private float distance = 0f;
        [SerializeField] private Camera cam;
        private Vector3 _currentVelocity;

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
            position = followTarget.position 
                       // + GetHalfOffset() 
                       + offset.normalized * distance;

            trans.forward = -offset.normalized;
            trans.position = position;
        }

        public static Camera GetCamera()
        {
            return _instance.cam;
        }

        public static Vector3 GetCameraForward(bool projectOnPlane = true)
        {
            var forward = _instance.cam.transform.forward;
            return (projectOnPlane ? Vector3.ProjectOnPlane(forward, Vector3.up) : forward).normalized;
        }
        public static Vector3 GetCameraRight(bool projectOnPlane = true)
        {
            var right = _instance.cam.transform.right;
            return (projectOnPlane ? Vector3.ProjectOnPlane(right, Vector3.up) : right).normalized;
        }
        public static Ray GetRay(Vector3? screenPoint = null)
        {
            return GetCamera().ScreenPointToRay(screenPoint ?? Input.mousePosition);
        }
    }
}