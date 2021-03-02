using UniRx;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using Arkademy.Spells;
using System.Collections;


namespace Arkademy.Pawns
{
    [RequireComponent(typeof(Rigidbody))]
    public class Pawn : MonoBehaviour
    {
        public Vector3 moveDirection;
        public Vector3 lookDirection;
        public bool CanMove => canMove;
        [SerializeField] private bool canMove = true;
        [SerializeField] private float maxMoveSpeed = 0f;
        [SerializeField] private float minDottedSpeed = 0.5f;
        [SerializeField] private float acceleration = 0f;
        [SerializeField] private float deceleration = 0f;
        [SerializeField] private float angularSpeed = 0;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private Quaternion rot;
        [SerializeField] private bool dash;
        [SerializeField] internal List<ISpell> spells;
        public ReadOnlyReactiveProperty<Interactable> CurrFocus => _currFocus;
        private ReadOnlyReactiveProperty<Interactable> _currFocus;
        public ReactiveCollection<Interactable> InteractablesInRange => _interactablesInRange;
        private ReactiveCollection<Interactable> _interactablesInRange = new ReactiveCollection<Interactable>();
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            _currFocus = GetInteractableRP();
            spells = GetSpells();
        }

        private List<ISpell> GetSpells(){
            // read data from local storage
            return new List<ISpell>{new IceBeam(), new FireBall()};
        }

        private ReadOnlyReactiveProperty<Interactable> GetInteractableRP(){
            return Observable.EveryUpdate().Select(x =>
            {
                if (!_interactablesInRange.Any())
                {
                    return null;
                }

                return _interactablesInRange
                            .OrderBy(x => Vector3.Distance(x.transform.position, transform.position))
                            .First();

            }).StartWith(null as Interactable).ToReadOnlyReactiveProperty();
        }

        private void Start()
        {
            lookDirection = transform.forward;
        }

        private void FixedUpdate()
        {
            rot = Quaternion.LookRotation(lookDirection, Vector3.up);
            var dottedMoveSpeed = maxMoveSpeed * (minDottedSpeed + (Vector3.Dot(lookDirection, moveDirection) + 1f) / 4f);
            var velocity = rb.velocity;
            var accel = moveDirection.sqrMagnitude.Equals(0f) ? deceleration : acceleration;
            rb.velocity = new Vector3(
                Mathf.Lerp(velocity.x, moveDirection.x * dottedMoveSpeed, accel * Time.fixedDeltaTime),
                velocity.y,
                Mathf.Lerp(velocity.z, moveDirection.z * dottedMoveSpeed, accel * Time.fixedDeltaTime)
            );
            
            rb.rotation = Quaternion.RotateTowards(rb.rotation, rot.normalized, Time.fixedDeltaTime * angularSpeed);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            var interactableComponent = other.GetComponent<Interactable>();
            if (interactableComponent == null) 
            {
                return;
            }
            _interactablesInRange.Add(interactableComponent);
        }

        private void OnTriggerExit(Collider other)
        {
            var interactableComponent = other.GetComponent<Interactable>();
            if (interactableComponent == null || !_interactablesInRange.Contains(interactableComponent))
            {
                return;
            }
            _interactablesInRange.Remove(interactableComponent);
        }

        public void InteractWithCurrFocus()
        {
            if (!_currFocus.HasValue || _currFocus.Value is null)
            {
                return;
            }
            _currFocus.Value.Interact(this);
        }
        
    }
}