using System;
using Arkademy.Abilities;
using UnityEngine;

namespace Arkademy
{
    public class AbilityInput : MonoBehaviour
    {
        public Player player;
        public Player.InputState state;
        public AbilityBase ability;
        
        private void Update()
        {
            if (!player || !ability) return;
            if (player.CurrInputState == state)
            {
                Debug.Log(this,this);
                ability.TryUse();
            }
        }
    }
}
