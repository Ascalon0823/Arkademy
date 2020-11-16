using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Arkademy
{
    [RequireComponent(typeof(Camera))]
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private Transform followTarget = null;
        [SerializeField]
        private Vector3 offset = Vector3.zero;
        [SerializeField]
        private float distance = 0f;
        [SerializeField]
        private uint smoothSpeed = 0;
        private void Update()
        {
            if (null == followTarget) return;
            var trans = transform;
            var position = trans.position;
            var oldPos = position;
            position = followTarget.position + offset.normalized * distance;
            trans.LookAt(followTarget);
            position = Vector3.Slerp(oldPos, position, Time.deltaTime * (1 + smoothSpeed));
            trans.position = position;
        }
    }

}
