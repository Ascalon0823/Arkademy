using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arkademy.ProjectileSystem
{
    [System.Serializable]
    public struct Projectile
    {
        public Vector3 dir;
        public float speed;
        public float radius;
        public float life;
    }
    public static class ProjectileFactory
    {
        public static ProjectileBehaviour SpawnProjectile(Projectile def)
        { 
            return new GameObject().AddComponent<ProjectileBehaviour>().InitUsing(def).SetVFXUsing();
        }
    }
}
