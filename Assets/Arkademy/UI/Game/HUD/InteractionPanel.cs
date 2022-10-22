using System;
using UnityEngine;
using UnityEngine.UI;

namespace Arkademy.UI.Game.HUD
{
    public class InteractionPanel : MonoBehaviour
    {
        [SerializeField] private CanvasGroup cg;

        [SerializeField] private Button cancelButton;

        private void Start()
        {
            cancelButton.onClick.RemoveAllListeners();
            cancelButton.onClick.AddListener(CancelInteraction);
        }

        private void CancelInteraction()
        {
            if (!Player.LocalPlayer) return;
            var playerActor = Player.LocalPlayer.currActor;
            if (!playerActor) return;
            var interact = playerActor.GetComponentInChildren<Interaction>();
            if (!interact) return;
            interact.StopInteraction();
        }

        void Update()
        {
            cg.interactable = false;
            cg.alpha = 0;
            cg.blocksRaycasts = false;
            if (!Player.LocalPlayer) return;
            var playerActor = Player.LocalPlayer.currActor;
            if (!playerActor) return;
            var interact = playerActor.GetComponentInChildren<Interaction>();
            if (!interact || !interact.currTarget) return;
            cg.interactable = true;
            cg.alpha = 1f;
            cg.blocksRaycasts = true;
        }
    }
}