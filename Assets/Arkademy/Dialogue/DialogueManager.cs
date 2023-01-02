using System;
using UnityEngine;

namespace Arkademy.Dialogue
{
    public class DialogueManager : MonoBehaviour
    {
        public static DialogueManager Instance;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
                return;
            }

            Instance = this;
        }

        public Character[] participants;
        public Character current;

        public void TerminateDialogue()
        {
            current = null;
            participants = null;
        }
    }
}