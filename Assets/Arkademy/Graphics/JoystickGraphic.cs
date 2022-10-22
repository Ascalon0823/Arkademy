using System;
using Lean.Touch;
using UnityEngine;

namespace Arkademy.Graphics
{
    public class JoystickGraphic : MonoBehaviour
    {
        [SerializeField] private RectTransform root;
        [SerializeField] private RectTransform handle;

        private void Start()
        {
            root.gameObject.SetActive(false);
        }

        void LateUpdate()
        {
            var fingers = LeanTouch.GetFingers(true,false,1);
            if (fingers == null||fingers.Count==0) return;
            var finger = fingers[0];
            if (finger.GetScreenDistance(finger.StartScreenPosition) >
                LeanTouch.CurrentSwipeThreshold)
            {
                root.gameObject.SetActive(true);
            }
            if (finger.Up)
            {
                root.gameObject.SetActive(false);
                return;
            }
            root.position = finger.StartScreenPosition;
            handle.position = finger.ScreenPosition;
        }
    }
}
