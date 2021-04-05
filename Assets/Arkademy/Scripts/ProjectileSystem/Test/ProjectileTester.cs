using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arkademy.ProjectileSystem.Test
{
    public class ProjectileTester : MonoBehaviour
    {
        public Transform shooterT;
        public Projectile projectile;
        void Update()
        {
            var ray = CameraController.GetRay();
            if (Input.GetMouseButtonUp(0) && Physics.Raycast(ray,out var hit))
            {
                Debug.Log("Shoot");
                projectile.dir = hit.point - shooterT.transform.position;
                ProjectileFactory.SpawnProjectile(projectile);
            }
        }
    }
}
