using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Arkademy
{
    
    public class Caster : MonoBehaviour
    {
        private Plane _casterPlane = new Plane(Vector3.zero, Vector3.up);
        public Transform castingPoint;
        public EnergySource resource;
        [SerializeField] private Spell currSpell;
        
        private void Update()
        {
            var pos = transform.position;
            _casterPlane.SetNormalAndPosition(Vector3.up, pos);
            var ray = CameraController.GetRay();
            var hit = _casterPlane.Raycast(ray, out var enter);
            var aim = ray.GetPoint(enter);
            if (Input.GetMouseButtonDown(0) && hit)
            {
                currSpell.OnDown(this,aim);
            }else  if (Input.GetMouseButton(0) && hit)
            {
                transform.rotation = Quaternion.LookRotation(aim - pos, Vector3.up);
                currSpell.OnHeld(this,aim);
            }
            else if (Input.GetMouseButtonUp(0) && hit)
            {
                currSpell.OnUp(this,aim);
            }
            else
            {
                transform.localRotation = Quaternion.identity;
            }
        }
    }
}