using System;
using System.Collections;
using System.Collections.Generic;
using Arkademy.Characters;
using Arkademy.Spells;
using Arkademy.GameStatus;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace Arkademy.UI.MainMenu
{
    public class UICharacterCreationPage : MonoBehaviour
    {
        [SerializeField] private TMP_InputField nameInputField;

        [SerializeField] private Button completeButton;
        private Character currChar;

        private void OnEnable()
        {
            if (nameInputField == null) return;
            completeButton.interactable = false;
            nameInputField.onValueChanged.AddListener(OnNameChanged);
            nameInputField.text = "Yun yun";
        }

        private void OnDisable()
        {
            if (nameInputField == null) return;
            nameInputField.onValueChanged.RemoveListener(OnNameChanged);
        }

        private void OnNameChanged(string name)
        {
            if (completeButton == null) return;
            completeButton.interactable = ValidName(name);
        }

        private bool ValidName(string name)
        {
            return !string.IsNullOrEmpty(name);
        }
        public void FinishCreation()
        {
            gameObject.SetActive(false);
            currChar = new Character(1, name, new List<ISpell>{new FireBall()});
            var save = new Save {
                Characters = new List<Character>{currChar},
                SaveTime = DateTime.Now
            };
            SystemManager.SaveGame(save);
            SystemManager.LoadGame();
        }
    }
 
}

