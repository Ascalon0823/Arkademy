using UnityEngine;
using UnityEngine.UI;
namespace Arkademy.UI.Game.HUD
{
    public class EnergyDisplay : MonoBehaviour
    {
        [SerializeField] private Image fill;
        [SerializeField] private CanvasGroup group;
        
        void Update()
        {
            if (!Player.LocalPlayer) return;
            var playerActor = Player.LocalPlayer.currActor;
            if (!playerActor) return;
            var energy = playerActor.GetComponentInChildren<Energy>();
            group.alpha = energy ? 1 : 0f;
            if (!energy) return;
            fill.fillAmount = energy.curr / energy.max;
        }
    }
}
