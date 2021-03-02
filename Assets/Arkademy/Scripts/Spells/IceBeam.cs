using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arkademy.Spells
{
    public class IceBeam : ISpell
    {
        public void Cast(CastEventData eventData)
        {
            switch (eventData.Status)
            {
                case CastStatus.Idle:
                    break;
                case CastStatus.Begin:
                    break;
                case CastStatus.Hold:
                    Debug.Log("Ice beam freezing");
                    break;
                case CastStatus.End:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

}
