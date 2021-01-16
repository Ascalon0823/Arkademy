using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;

namespace Arkademy
{
    [RequireComponent(typeof(Pawn))]
    public class PawnController : MonoBehaviour
    {
        public Pawn CurrPawn
        {
            get => currPawn;
            private set => currPawn = value;
        }
        [SerializeField] private Pawn currPawn;


        private void Awake()
        {
            currPawn = GetComponent<Pawn>();
        }

        private void Start()
        {
            currPawn.CurrFocus.DistinctUntilChanged()
                .Subscribe(Highlighter.HighlightOn).AddTo(this);
        }

        void HandleMovement()
        {
            if (!currPawn.CanMove) return;
            var dir = Vector3.zero;
            var forward = CameraController.GetCameraForward();
            var right = CameraController.GetCameraRight();
            if (Input.GetKey(KeyCode.W))
            {
                dir += forward;
            }

            if (Input.GetKey(KeyCode.S))
            {
                dir -= forward;
            }

            if (Input.GetKey(KeyCode.A))
            {
                dir -= right;
            }

            if (Input.GetKey(KeyCode.D))
            {
                dir += right;
            }
            
            currPawn.moveDirection = dir.normalized;
            var p = new Plane(Vector3.up,currPawn.transform.position);
            var ray = CameraController.GetRay();
            currPawn.lookDirection = (p.Raycast(ray, out var enter) && Input.GetMouseButton(0)
                ? Vector3.ProjectOnPlane(ray.GetPoint(enter) - currPawn.transform.position, Vector3.up)
                : (currPawn.moveDirection.magnitude.Equals(0f) ? currPawn.transform.forward : currPawn.moveDirection)).normalized;
        }

        void HandleInteraction()
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                currPawn.InteractWithCurrFocus();
            }
        }

        // Update is called once per frame
        void Update()
        {
            HandleMovement();
            HandleInteraction();
        }
    
    }
}