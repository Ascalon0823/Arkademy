using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Arkademy.Spells
{
    public class FireBall : ISpell
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
                    break;
                case CastStatus.End:
                    Debug.Log("Shoot fire ball");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}