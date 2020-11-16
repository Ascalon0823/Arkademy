using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arkademy
{
    [RequireComponent(typeof(Rigidbody))]
    public class Pawn : MonoBehaviour
    {
        public Vector3 moveDirection;
        public Vector3 lookDirection;
        private Plane _pawnPlane = new Plane(Vector3.zero, Vector3.up);
        [SerializeField] private float maxMoveSpeed = 0f;
        [SerializeField] private float acceleration = 0f;
        [SerializeField] private float angularSpeed = 0;
        [SerializeField] private Rigidbody rb;

        [SerializeField] private Quaternion rot;
        [SerializeField] private bool dash;

        // Start is called before the first frame update
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            var dir = Vector3.zero;
            var forward = Vector3.ProjectOnPlane(CameraController.GetCamera().transform.forward, Vector3.up).normalized;
            var right = Vector3.ProjectOnPlane(CameraController.GetCamera().transform.right, Vector3.up).normalized;
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
            moveDirection = dir.normalized;
            _pawnPlane.SetNormalAndPosition(Vector3.up, rb.transform.position);
            var ray = CameraController.GetRay();
            lookDirection = (_pawnPlane.Raycast(ray, out var enter) && Input.GetMouseButton(0)
                ? Vector3.ProjectOnPlane(ray.GetPoint(enter) - rb.position, Vector3.up)
                : (moveDirection.magnitude.Equals(0f) ? rb.transform.forward : moveDirection)).normalized;
            rot = Quaternion.LookRotation(lookDirection, Vector3.up);
        }


        private void FixedUpdate()
        {
            var dottedMoveSpeed = maxMoveSpeed * (0.5f + (Vector3.Dot(lookDirection, moveDirection) + 1f) / 4f);
            var velocity = rb.velocity;
            rb.velocity = new Vector3(
                Mathf.Lerp(velocity.x, moveDirection.x * dottedMoveSpeed, acceleration * Time.fixedDeltaTime),
                velocity.y,
                Mathf.Lerp(velocity.z, moveDirection.z * dottedMoveSpeed, acceleration * Time.fixedDeltaTime)
            );
            rb.rotation = Quaternion.RotateTowards(rb.rotation, rot.normalized, Time.fixedDeltaTime * angularSpeed);
        }
    }
}