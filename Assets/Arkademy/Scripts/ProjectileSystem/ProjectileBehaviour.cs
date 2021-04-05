using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

namespace Arkademy.ProjectileSystem
{
    public class ProjectileBehaviour : MonoBehaviour
    {
        [SerializeField] private Projectile projectile;
        [SerializeField] private Collider[] hits = new Collider[100];
        [SerializeField] private List<GameObject> vfxs = new List<GameObject>();
        public ProjectileBehaviour InitUsing(Projectile proj)
        {
            projectile = proj;
            foreach (var vfx in vfxs)
            {
                vfx.transform.localScale = Vector3.one * projectile.radius;
            }
            return this;
        }

        public ProjectileBehaviour SetVFXUsing(GameObject go = null)
        {
            if (go == null)
            {
                go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            }
            go.transform.SetParent(transform,false);
            DestroyImmediate(go.GetComponent<Collider>());
            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = Quaternion.identity;
            go.transform.localScale = Vector3.one * projectile.radius;
            vfxs.Add(go);
            return this;
        }
        // Update is called once per frame
        void FixedUpdate()
        {
            projectile.life -= Time.fixedDeltaTime;
            if (projectile.life <= 0f)
            {
                Destroy(gameObject);
                return;
            }
            transform.position += projectile.dir.normalized * (projectile.speed * Time.fixedDeltaTime);
            if (Physics.OverlapSphereNonAlloc(transform.position, projectile.radius, hits) == 0) return;
        }
    }
}
