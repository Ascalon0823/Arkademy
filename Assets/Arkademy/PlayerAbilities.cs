using System;
using System.Collections.Generic;
using Arkademy.Abilities;
using UnityEngine;

namespace Arkademy
{
    public class PlayerAbilities : MonoBehaviour
    {
        public Player player;
        public AbilityBase tap;
        public AbilityBase swipe;
        public AbilityBase drag;
        public AbilityBase hold;
        public AbilityBase dragSwipe;

        private void Update()
        {
            if (!player) return;
            if (player.tap && tap)
            {
                tap.TryUse();
            }

            if (!player.postSwipeDrag && player.swipe && swipe)
            {
                swipe.TryUse();
            }


            if (player.hold && player.up && !player.drag && hold)
            {
                hold.TryUse();
            }

            if (player.postSwipeDrag)
            {
                if (player.swipe && dragSwipe)
                {
                    dragSwipe.TryUse();
                }
                else
                {
                    if (drag)
                    {
                        drag.TryUse();
                    }
                }
            }
        }
    }
}