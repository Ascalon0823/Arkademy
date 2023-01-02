using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Arkademy.UI.Game.HUD
{
    public class InteractCandidatePopup : MonoBehaviour
    {
        public CanvasGroup cg;
        public Image targetSpriteHolder;
        public TextMeshProUGUI targetName;


        private void Update()
        {
            cg.interactable = false;
            cg.alpha = 0;
            cg.blocksRaycasts = false;
            if (!Player.LocalPlayer||!Player.LocalPlayer.currActor
            ||!Player.LocalPlayer.currActor.interaction) return;
            if (!Player.LocalPlayer.currActor.interaction.currCandidate) return;
            
            cg.interactable = true;
            cg.alpha = 1f;
            cg.blocksRaycasts = true;
            // var sprite = Player.LocalPlayer.currActor.interaction..transform.root.GetComponentsInChildren<SpriteRenderer>()[1];
            // targetSpriteHolder.sprite = sprite.sprite;
            // targetName.text = Player.LocalPlayer.currInteractionCandidate.name;
        }
    }
}