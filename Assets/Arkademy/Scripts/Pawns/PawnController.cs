using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;

namespace Arkademy.Pawns
{
    [RequireComponent(typeof(Pawn))]
    public class PawnController : MonoBehaviour
    {
        [SerializeField] private SpellCaster caster;
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
            currPawn.CurrFocus
                .DistinctUntilChanged()
                .Subscribe(Highlighter.HighlightOn)
                .AddTo(this);
        }

        private void HandleMovement()
        {
            if (!currPawn.CanMove) 
            {
                return;
            }
            currPawn.moveDirection = GetDirection();
            currPawn.lookDirection = GetLookDirection();
        }

        private Vector3 GetLookDirection(){
            var p = new Plane(Vector3.up,currPawn.transform.position);
            var ray = CameraController.GetRay();

            var hasLookDirection = CameraController.GetPlaneRayHit(p,out var hit) && Input.GetMouseButton(0);
            if (hasLookDirection){
                return Vector3.ProjectOnPlane(hit - currPawn.transform.position, Vector3.up);
            }

            var hasMoveDirection = !currPawn.moveDirection.magnitude.Equals(0f);
            if (hasMoveDirection){
                return currPawn.moveDirection.normalized;
            }

            return currPawn.transform.forward;
        }

        private Vector3 GetDirection(){
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

            return dir.normalized;;
        }

        private void HandleInteraction()
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                currPawn.InteractWithCurrFocus();
            }
        }

        private void HandleSpellLoading(){
            // todo: update logics
            if (Input.GetKeyDown(KeyCode.Alpha1)){
                // handle user interaction to decide which spell to use
                // mock 
                var selectedSpell = currPawn.spells[0];
                caster.LoadSpell(selectedSpell);
            }
        }

        // Update is called once per frame
        private void Update()
        {
            HandleSpellLoading();
            HandleMovement();
            HandleInteraction();
        }
    
    }
}