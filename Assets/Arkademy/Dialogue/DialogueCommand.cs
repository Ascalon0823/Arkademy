using System;
using UnityEngine;

namespace Arkademy.Dialogue
{
    [Serializable]
    public class DialogueCommand
    {
        public string displayText;
        public string dialogue;
        public static DialogueCommand Greet = new DialogueCommand {displayText = "Greet"};
        public static DialogueCommand Bye = new DialogueCommand {displayText = "Bye"};
    }
}
