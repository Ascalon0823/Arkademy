using System;
using System.Collections.Generic;
using Arkademy.Abilities;
using UnityEngine;

namespace Arkademy
{
    public class PlayerAbilities : MonoBehaviour
    {
        public Player player;
        public AbilityInput[] abilityInputs;

        private void Update()
        {
            if (!player) return;
        }
    }
}