using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Arkademy.PropertySystem;
namespace Arkademy.EffectSystem
{
    public class KinectEffect : Effect
    {
        public KinectEffect(float energy, Vector3 dir, Vector3 pos) : base(energy, dir, pos)
        {
        }

        public override void Apply(PropertiesHolder holder)
        {
            if (holder.GetComponent<Rigidbody>())
            {
                holder.GetComponent<Rigidbody>().AddForceAtPosition(direction.normalized*magnitude,position);
            }
        }
    }
}
