using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Arkademy.UI.Game.HUD
{
    public class InteractionSelector : MonoBehaviour
    {
        public CanvasGroup cg;
        public Image targetSpriteHolder;
        public TextMeshProUGUI targetName;
        

        private void Update()
        {
            cg.interactable = false;
            cg.alpha = 0;
            cg.blocksRaycasts = false;
            if (!Player.LocalPlayer) return;
            var playerActor = Player.LocalPlayer.currActor;
            if (!playerActor) return;
            var detector = playerActor.GetComponentInChildren<Detector>();
            if (!detector) return;
            if (detector.Detected == null || detector.Detected.Count == 0) return;
            var validDetected = detector.Detected.Where(x => x.gameObject.layer == LayerMask.NameToLayer("Default")&&!x.isTrigger).ToList();
            if (!validDetected.Any()) return;
            
            var nearest =
                validDetected.OrderBy(x => Vector2.Distance(x.transform.position, detector.transform.position))
                    .First();
            cg.interactable = true;
            cg.alpha = 1f;
            cg.blocksRaycasts = true;
            var sprite = nearest.transform.root.GetComponentsInChildren<SpriteRenderer>()[1];
            targetSpriteHolder.sprite = sprite.sprite;
            targetName.text = nearest.transform.root.name;
        }
    }
}