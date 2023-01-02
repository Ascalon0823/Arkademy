using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Arkademy.UI.Game.HUD
{
    public class DialoguePanel : MonoBehaviour
    {
        [SerializeField] private CanvasGroup cg;
        [SerializeField] private Transform dialogueList;
        [SerializeField] private TextMeshProUGUI dialogueTextPrefab;
        [SerializeField] private DialogueCommandOption commandOptionPrefab;
        
        private void Update()
        {
            cg.interactable = false;
            cg.alpha = 0;
            cg.blocksRaycasts = false;
            if (!Player.LocalPlayer || !Player.LocalPlayer.currActor) return;
            if (!Dialogue.DialogueManager.Instance || Dialogue.DialogueManager.Instance.participants?.Length==0) return;
            cg.interactable = true;
            cg.alpha = 1f;
            cg.blocksRaycasts = true;
        }
    }
}