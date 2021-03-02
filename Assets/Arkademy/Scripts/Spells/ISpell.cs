using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arkademy.Spells {
    public interface ISpell
    {
        public void Cast(CastEventData eventData);
    }
}
