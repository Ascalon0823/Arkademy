using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Arkademy
{
    [RequireComponent(typeof(Rigidbody))]
    public class PawnController : MonoBehaviour
    {
        public Vector3 moveDirection;
        public Vector3 lookDirection;
        private Plane _pawnPlane = new Plane(Vector3.zero, Vector3.up);
        [SerializeField] private float maxMoveSpeed = 0f;
        [SerializeField] private float minDottedSpeed = 0.5f;
        [SerializeField] private float acceleration = 0f;
        [SerializeField] private float angularSpeed = 0;
        [SerializeField] private Rigidbody rb;

        [SerializeField] private Quaternion rot;
        [SerializeField] private bool dash;
        [SerializeField] private Interactable currentFocus = null;
        private List<Interactable> _interactablesInRange = new List<Interactable>();

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        void HandleMovement()
        {
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

            moveDirection = dir.normalized;
            _pawnPlane.SetNormalAndPosition(Vector3.up, rb.transform.position);
            var ray = CameraController.GetRay();
            lookDirection = (_pawnPlane.Raycast(ray, out var enter) && Input.GetMouseButton(0)
                ? Vector3.ProjectOnPlane(ray.GetPoint(enter) - rb.position, Vector3.up)
                : (moveDirection.magnitude.Equals(0f) ? rb.transform.forward : moveDirection)).normalized;
            rot = Quaternion.LookRotation(lookDirection, Vector3.up);
        }

        void HandleInteraction()
        {
            if (!_interactablesInRange.Any())
            {
                if(currentFocus!=null)
                    currentFocus.Highlight(gameObject,false);
                currentFocus = null;
                return;
            }
            var nearest = _interactablesInRange.OrderBy(
                x => Vector3.Distance(x.transform.position, transform.position)).First();
            if (currentFocus != nearest)
            {
                if(currentFocus!=null)
                    currentFocus.Highlight(gameObject,false);
                currentFocus = nearest;
                nearest.Highlight(gameObject,true);
            }
            if (Input.GetKeyUp(KeyCode.E))
            {
                nearest.Interact(gameObject);
            }
        }

        // Update is called once per frame
        void Update()
        {
            HandleMovement();
            HandleInteraction();
        }

        private void FixedUpdate()
        {
            var dottedMoveSpeed =
                maxMoveSpeed * (minDottedSpeed + (Vector3.Dot(lookDirection, moveDirection) + 1f) / 4f);
            var velocity = rb.velocity;
            rb.velocity = new Vector3(
                Mathf.Lerp(velocity.x, moveDirection.x * dottedMoveSpeed, acceleration * Time.fixedDeltaTime),
                velocity.y,
                Mathf.Lerp(velocity.z, moveDirection.z * dottedMoveSpeed, acceleration * Time.fixedDeltaTime)
            );
            rb.rotation = Quaternion.RotateTowards(rb.rotation, rot.normalized, Time.fixedDeltaTime * angularSpeed);
        }

        private void OnTriggerEnter(Collider other)
        {
            var inter = other.GetComponent<Interactable>();
            if (inter == null) return;
            _interactablesInRange.Add(inter);
        }

        private void OnTriggerExit(Collider other)
        {
            var inter = other.GetComponent<Interactable>();
            if (inter == null || !_interactablesInRange.Contains(inter)) return;
            _interactablesInRange.Remove(inter);
        }
    }
}