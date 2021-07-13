using System;
using Arkademy.Game;
using UnityEngine;

namespace DebugHelper.CameraDebug
{
    public class CameraDebug : MonoBehaviour
    {
#if !UNITY_EDITOR
        private void Awake()
        {
            Destroy(gameObject);
        }
#endif
#if UNITY_EDITOR
        void Update()
        {
            transform.position = PlayerCamera.Current.PointAtPos();
        }
#endif
    }
}