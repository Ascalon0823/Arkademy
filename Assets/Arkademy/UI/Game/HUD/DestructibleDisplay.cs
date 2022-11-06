using UnityEngine;
using UnityEngine.UI;

namespace Arkademy.UI.Game.HUD
{
    public class DestructibleDisplay : MonoBehaviour
    {
        [SerializeField] private Image fill;
        [SerializeField] private CanvasGroup group;

        void Update()
        {
            if (!Player.LocalPlayer) return;
            var playerActor = Player.LocalPlayer.currActor;
            if (!playerActor) return;
            var destructible = playerActor.GetComponentInChildren<Destructible>();
            group.alpha = destructible ? 1 : 0f;
            if (!destructible) return;
            fill.fillAmount = destructible.currDurability * 1.0f / destructible.maxDurability;
        }
    }
}