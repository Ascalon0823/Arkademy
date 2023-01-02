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
        public ActorBehaviour currActor;

        public PlayerTouchInput playerTouchInput;

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

            playerTouchInput.UpdateInput();
            HandleMove();
            HandleInteraction();
        }


        private void HandleMove()
        {
            if (!currActor) return;
            if (!currActor.motor) return;
            if (playerTouchInput.tap || playerTouchInput.up)
            {
                currActor.motor.moveDir = Vector2.zero;
                return;
            }

            if (playerTouchInput.drag)
            {
                currActor.motor.moveDir = playerTouchInput.dragDistance * 4 / Screen.width;
                currActor.motor.moveDir = Vector2.ClampMagnitude(currActor.motor.moveDir, 1f);
            }
        }

        private void HandleInteraction()
        {
            if (!currActor  || !playerTouchInput || !currActor.interaction)
            {
                return;
            }

            if (playerTouchInput.tap)
                currActor.interaction.InteractWithCurrentCandidate();
        }
    }
}