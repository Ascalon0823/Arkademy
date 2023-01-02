using Arkademy.Dialogue;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Arkademy.UI.Game.HUD
{
    public class DialogueCommandOption : MonoBehaviour
    {
        public DialogueCommand command;
        public DialoguePanel parentPanel;
        [SerializeField] private TextMeshProUGUI text;

        public void Setup(DialogueCommand newCommand, DialoguePanel parent)
        {
            command = newCommand;
            parentPanel = parent;
            text.text = command.displayText;
        }

        public void OnClick()
        {
        }
    }
}