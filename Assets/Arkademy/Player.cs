using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Lean.Touch;
using UnityEngine;

namespace Arkademy
{
    public class Player : MonoBehaviour
    {
        public static Player LocalPlayer;
        public GameObject currActor;

        private void Awake()
        {
            if (LocalPlayer != null)
            {
                Destroy(this);
                return;
            }

            LocalPlayer = this;
        }

        private void Update()
        {
            if (ApplicationManager.Paused)
            {
                return;
            }

            HandleInput();
        }

        private void HandleInput()
        {
            if (!currActor) return;
            var motor = currActor.GetComponent<Motor>();
            if (!motor) return;
            var fingers = LeanTouch.GetFingers(true, false, 1);
            if (fingers == null || fingers.Count == 0) return;
            var finger = fingers[0];
            if (finger.Age < LeanTouch.CurrentTapThreshold && finger.GetScreenDistance(finger.StartScreenPosition) <
                LeanTouch.CurrentSwipeThreshold) return;
            if (finger.Tap)
            {
                Debug.Log("Tap");
                motor.moveDir = Vector2.zero;
                return;
            }

            if (finger.Up)
            {
                if (finger.GetSnapshotScreenDelta(LeanTouch.CurrentTapThreshold).magnitude >= LeanTouch.CurrentSwipeThreshold)
                {
                    
                    Debug.Log("SwipeEnd");
                }
                motor.moveDir = Vector2.zero;
                return;
            }

            motor.moveDir = (finger.ScreenPosition - finger.StartScreenPosition) * 4 / Screen.width;
            motor.moveDir = Vector2.ClampMagnitude(motor.moveDir, 1f);
        }
    }
}