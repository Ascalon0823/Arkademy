using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Arkademy.UI.Game.HUD
{
    public class DialoguePanel : MonoBehaviour
    {
        [SerializeField] private CanvasGroup cg;

        [SerializeField] private TextMeshProUGUI dialogueTextPrefab;
        [SerializeField] private DialogueCommandOption commandOptionPrefab;
        


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
            if (!Player.LocalPlayer || !Player.LocalPlayer.currActor || !Player.LocalPlayer.currActor.interaction ||
                !Player.LocalPlayer.currActor.interaction.currTarget) return;
            var actorBehaviour = Player.LocalPlayer.currActor.interaction.currTarget.GetComponent<ActorBehaviour>();
            if (!actorBehaviour || actorBehaviour.character == null) return;
            cg.interactable = true;
            cg.alpha = 1f;
            cg.blocksRaycasts = true;
        }
    }
}