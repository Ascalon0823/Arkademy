using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Arkademy.Graphics;
using Lean.Touch;
using UnityEngine;

namespace Arkademy
{
    public class Player : MonoBehaviour
    {
        public static Player LocalPlayer;
        public GameObject currActor;
        public Interaction currInteractionCandidate;

        public float interactionTime;
        private bool fingerInUse;
        private bool fingerDisposed;
        public bool tap;
        public bool swipe;
        public bool drag;
        public bool hold;
        public bool up;
        public Vector2 dragDistance;
        public Vector2 swipeDistance;
        public float holdTresh;
        public float holdTime;

        public ProjectileBehaviour spawn;
        private void Awake()
        {
            if (LocalPlayer != null)
            {
                Destroy(this);
                return;
            }

            LocalPlayer = this;
            Application.targetFrameRate = 60;
        }

        private void Start()
        {
            LeanTouch.Instance.SwipeThreshold = Screen.width / 8;
        }

        private void Update()
        {
            if (ApplicationManager.Paused)
            {
                return;
            }

            HandleInput();
            HandleMove();
            HandleInteractionCandidate();
        }

        private LeanFinger GetFinger()
        {
            var fingers = LeanTouch.GetFingers(true, false, 1);
            if (fingers == null || fingers.Count == 0) return null;
            return fingers[0];
        }

        private void HandleInput()
        {
            var finger = GetFinger();
            if (finger == null)
            {
                tap = false;
                up = false;
                hold = false;
                drag = false;
                swipe = false;
                return;
            }

            if (fingerDisposed)
            {
                if (finger.Up)
                {
                    fingerDisposed = false;
                }

                return;
            }

            tap = finger.Tap;
            up = finger.Up;
            swipe = finger.Swipe || (up && finger.GetSnapshotScreenDelta(LeanTouch.CurrentTapThreshold).magnitude >=
                LeanTouch.CurrentSwipeThreshold);

            drag = finger.GetScreenDistance(finger.StartScreenPosition) >
                LeanTouch.CurrentSwipeThreshold || drag;

            hold = finger.Age > holdTresh && finger.GetScreenDistance(finger.StartScreenPosition) <
                LeanTouch.CurrentSwipeThreshold && !drag || hold;
            if (tap)
            {
                Debug.Log("tap");
                var p = Instantiate(spawn, currActor.transform.position, Quaternion.identity);
                if (currInteractionCandidate)
                {
                    p.targetTransform = currInteractionCandidate.transform;
                }

                p.ignores = new[] {currActor};
            }

            if (up)
            {
                Debug.Log("up");
            }

            if (swipe)
            {
                swipeDistance = finger.GetSnapshotScreenDelta(LeanTouch.CurrentTapThreshold);
                Debug.Log("swipe");
            }

            if (drag)
            {
                dragDistance = finger.ScreenPosition - finger.StartScreenPosition;
                Debug.Log("drag");
            }

            if (hold)
            {
                holdTime = finger.Age - LeanTouch.CurrentTapThreshold;
                Debug.Log("hold");
            }
        }


        private void HandleMove()
        {
            if (!currActor) return;
            var motor = currActor.GetComponent<Motor>();
            if (!motor) return;
            if (tap || up)
            {
                motor.moveDir = Vector2.zero;
                return;
            }

            if (drag)
            {
                motor.moveDir = dragDistance * 4 / Screen.width;
                motor.moveDir = Vector2.ClampMagnitude(motor.moveDir, 1f);
            }
        }

        private void HandleInteractionCandidate()
        {
            Interaction newInteractionCandidate = null;
            if (currActor)
            {
                var playerInteraction = currActor.GetComponentInChildren<Interaction>();
                if (playerInteraction)
                {
                    newInteractionCandidate = playerInteraction.currCandidate;
                }
            }

            if (newInteractionCandidate == currInteractionCandidate) return;
            if (newInteractionCandidate)
            {
                newInteractionCandidate.transform.root.GetComponentInChildren<HighlightControl>()
                    ?.ToggleHighlight(true);
            }

            if (currInteractionCandidate)
            {
                currInteractionCandidate.transform.root.GetComponentInChildren<HighlightControl>()
                    ?.ToggleHighlight(false);
            }

            currInteractionCandidate = newInteractionCandidate;
        }

        public void DisposeAllInput()
        {
            fingerDisposed = true;
        }
    }
}