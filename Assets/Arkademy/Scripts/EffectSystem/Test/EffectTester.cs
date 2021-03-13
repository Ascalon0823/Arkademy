using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arkademy.EffectSystem.Test
{
    public class EffectTester : MonoBehaviour
    {
        public Effect.Type effectType;

        public float energy;
        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonUp(0) )
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit) && PropertiesHolder.TryGetPropHolderFromCollider(hit.collider,out var holder))
                {
                    holder.ReceiveEffect(effectType.NewEffect(energy));
                }
            }
        }
    }

}
