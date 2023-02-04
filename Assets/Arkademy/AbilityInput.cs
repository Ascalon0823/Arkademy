using System;
using Arkademy.Abilities;
using UnityEngine;

namespace Arkademy
{
    public class AbilityInput : MonoBehaviour
    {
        public PlayerTouchInput input;
        public PlayerTouchInput.InputState state;
        public AbilityBase ability;
        
        private void Update()
        {
            if (!input || !ability) return;
            if (input.CurrInputState == state)
            {
                Debug.Log(this,this);
                ability.TryUse();
            }
        }
    }
}
