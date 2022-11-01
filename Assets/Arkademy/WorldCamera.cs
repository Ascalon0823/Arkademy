using System;
using Lean.Touch;
using UnityEngine;

namespace Arkademy
{
    public class WorldCamera : MonoBehaviour
    {
        [SerializeField] private float deceleration;
        [SerializeField] private Vector2 currVelocity;
        [SerializeField] private Vector3? beginPos;
        [SerializeField] private Vector2 posDiff;

        private void Update()
        {
            HandleInput();
        }

        private void HandleInput()
        {
            var finger = GetFinger();
            if (finger == null) return;
            if (finger.Tap)
            {
                Debug.Log("Tap");
                if (currVelocity.magnitude > 0f)
                {
                    currVelocity = Vector2.zero;
                }
                else
                {
                    Debug.Log("Select");
                }

                beginPos = null;
                return;
            }

            if (finger.Age < LeanTouch.CurrentTapThreshold && finger.GetScreenDistance(finger.StartScreenPosition) <
                LeanTouch.CurrentSwipeThreshold)
            {
                beginPos = null;
                return;
            }


            if (finger.Up)
            {
                if (finger.GetSnapshotScreenDelta(LeanTouch.CurrentTapThreshold).magnitude >=
                    LeanTouch.CurrentSwipeThreshold * 2f)
                {
                    Debug.Log("SwipeEnd");
                    currVelocity = finger.GetWorldPosition(-10) -
                                   finger.GetSnapshotWorldPosition(finger.Age - LeanTouch.CurrentTapThreshold, -10f);
                    
                }
                beginPos = null;
                return;
            }

            if (finger.GetScreenDistance(finger.StartScreenPosition) >
                LeanTouch.CurrentSwipeThreshold && !beginPos.HasValue)
            {
                beginPos = transform.position;
                currVelocity = Vector2.zero;
            }

            posDiff = finger.GetWorldPosition(-10) - finger.GetStartWorldPosition(-10);
        }

        private void LateUpdate()
        {
            HandlePosition();
        }

        private void HandlePosition()
        {
            if (beginPos.HasValue)
            {
                transform.position = beginPos.Value - (Vector3) posDiff;
                return;
            }

            if (currVelocity.magnitude == 0) return;
            transform.position -= (Vector3) (currVelocity * Time.deltaTime);
            var currSpeed = currVelocity.magnitude;
            currSpeed -= deceleration * Time.deltaTime;
            currSpeed = Mathf.Max(0f, currSpeed);
            currVelocity = currSpeed * currVelocity.normalized;
        }

        private LeanFinger GetFinger()
        {
            var fingers = LeanTouch.GetFingers(true, false, 1);
            if (fingers == null || fingers.Count == 0) return null;
            return fingers[0];
        }
    }
}