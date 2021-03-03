using System;
using UnityEngine;
using Arkademy.Spells.SpellEffects;

namespace Arkademy.Spells
{
     [Serializable]
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
                    Debug.Log("IceBeam");
                    SpellEffectFactory.Beam(eventData.CastOrigin, eventData.PointerPos, eventData.CastedTime);
                    break;
                case CastStatus.End:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

}
