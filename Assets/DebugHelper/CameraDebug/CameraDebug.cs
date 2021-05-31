using System;
using Arkademy.Scenes.Game;
using UnityEngine;

namespace DebugHelper.CameraDebug
{
    public class CameraDebug : MonoBehaviour
    {
        #if UNITY_EDITOR
        void Update()
        {
            transform.position = PlayerCamera.Current.PointAt();
        }
        #endif
    }
}
