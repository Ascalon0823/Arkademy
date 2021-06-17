using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Arkademy.Game;
namespace DebugHelper.CasterDebug
{
    public class CasterDebug : MonoBehaviour
    {
        public Caster caster;
#if UNITY_EDITOR
        void Update()
        {
            transform.position = caster.lastCastEvent.currPos;
        }
#endif

    }

}
