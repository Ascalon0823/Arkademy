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
        public GameObject currInteractionCandidate;

        public float interactionTime;
        private bool fingerInUse;
        private bool fingerDisposed;

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

            HandleMove();
            HandleInteractionInput();
            HandleInteractionCandidate();
        }

        private void LateUpdate()
        {
            HandleFingerExpire();
        }

        private void HandleFingerExpire()
        {
            var finger = GetFinger();
            if (finger == null) return;
            if (!finger.Up) return;
            fingerDisposed = false;
            fingerInUse = false;
        }

        private void HandleInteractionInput()
        {
            if (!currActor) return;
            var progress = currActor.GetComponentInChildren<ActionProgress>();
            if (!progress) return;
            progress.currProgress = GetInteractionProgress();
            if (progress.currProgress <= 1f) return;
            var interact = currActor.GetComponentInChildren<Interaction>();
            if (interact.currTarget) return;
            var other = currInteractionCandidate.GetComponentInChildren<Interaction>();
            if (!interact || !other) return;
            Debug.Log($"Interact with {currInteractionCandidate.name}", currInteractionCandidate);
            interact.InteractWith(other);
            fingerDisposed = true;
        }


        private float GetInteractionProgress()
        {
            if (!currInteractionCandidate) return 0f;
            var finger = GetFinger();
            if (finger == null) return 0f;
            if (finger.GetScreenDistance(finger.StartScreenPosition) >
                LeanTouch.CurrentSwipeThreshold) return 0f;
            if (fingerInUse||fingerDisposed) return 0f;
            return finger.Age < LeanTouch.CurrentTapThreshold ? 0f : finger.Age / interactionTime;
        }

        private void HandleInteractionCandidate()
        {
            var candidate = GetInteractionCandidate();
            if (candidate == currInteractionCandidate) return;
            if (candidate)
            {
                var hightlight = candidate.GetComponentInChildren<HighlightControl>();
                if (hightlight)
                {
                    hightlight.ToggleHighlight(true);
                }
            }

            if (currInteractionCandidate)
            {
                var hightlight = currInteractionCandidate.GetComponentInChildren<HighlightControl>();
                if (hightlight)
                {
                    hightlight.ToggleHighlight(false);
                }
            }

            currInteractionCandidate = candidate;
        }

        private GameObject GetInteractionCandidate()
        {
            if (!currActor) return null;
            var detector = currActor.GetComponentInChildren<Detector>();
            if (!detector) return null;
            if (detector.Detected == null || detector.Detected.Count == 0) return null;
            var validDetected = detector.Detected
                .Where(x => x.gameObject.layer == LayerMask.NameToLayer("Default") && !x.isTrigger).ToList();
            if (!validDetected.Any()) return null;
            var nearest =
                validDetected.OrderBy(x => Vector2.Distance(x.transform.position, detector.transform.position))
                    .First();
            return nearest.transform.root.gameObject;
        }

        private LeanFinger GetFinger()
        {
            var fingers = LeanTouch.GetFingers(true, false, 1);
            if (fingers == null || fingers.Count == 0) return null;
            return fingers[0];
        }

        private void HandleMove()
        {
            if (!currActor) return;
            var motor = currActor.GetComponent<Motor>();
            if (!motor) return;
            var finger = GetFinger();
            if (finger == null) return;
            if (fingerDisposed) return;
            if (finger.Age < LeanTouch.CurrentTapThreshold && finger.GetScreenDistance(finger.StartScreenPosition) <
                LeanTouch.CurrentSwipeThreshold)
            {
                return;
            }

            if (finger.Tap)
            {
                Debug.Log("Tap");
                motor.moveDir = Vector2.zero;
                return;
            }

            if (finger.Up)
            {
                if (finger.GetSnapshotScreenDelta(LeanTouch.CurrentTapThreshold).magnitude >=
                    LeanTouch.CurrentSwipeThreshold)
                {
                    Debug.Log("SwipeEnd");
                }
                motor.moveDir = Vector2.zero;
                return;
            }

            if (finger.GetScreenDistance(finger.StartScreenPosition) >
                LeanTouch.CurrentSwipeThreshold)
            {
                fingerInUse = true;
            }

            motor.moveDir = (finger.ScreenPosition - finger.StartScreenPosition) * 4 / Screen.width;
            motor.moveDir = Vector2.ClampMagnitude(motor.moveDir, 1f);
        }
    }
}