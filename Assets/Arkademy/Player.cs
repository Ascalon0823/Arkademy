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
            var fingers = LeanTouch.GetFingers(true,false,1);
            if (fingers == null||fingers.Count==0) return;
            var finger = fingers[0];
            if (finger.Up)
            {
                motor.moveDir = Vector2.zero;
                return;
            }
            motor.moveDir = (finger.ScreenPosition - finger.StartScreenPosition)*4/Screen.width;
            motor.moveDir = Vector2.ClampMagnitude(motor.moveDir,1f);
        }
    }
}