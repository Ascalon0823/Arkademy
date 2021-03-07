using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Arkademy.GameStatus;
namespace Arkademy.UI.MainGame
{
    public class UIPauseMenu : MonoBehaviour
    {
        private static UIPauseMenu _currInstance;

        public static void TogglePauseMenu()
        {
            if (_currInstance == null)
            {
                TogglePauseMenu(true);
                return;
            }
            _currInstance.gameObject.SetActive(!_currInstance.gameObject.activeSelf);
        }
        public static void TogglePauseMenu(bool active)
        {
            if (_currInstance == null)
            {
                var prefab = Resources.Load<UIPauseMenu>("UIPauseMenu");
                _currInstance = Instantiate(prefab);
            }
            _currInstance.gameObject.SetActive(active);
        }

        [SerializeField] private Button save;
        [SerializeField] private Button backToMain;
        [SerializeField] private Button settings;
        [SerializeField] private Button resume;

        private void OnEnable()
        {
            save.onClick.AddListener(HandleSave);
            backToMain.onClick.AddListener(SystemManager.GoToMainMenu);
            //Todo: replace with pause unpause call later
            resume.onClick.AddListener(HandleResume);
            
        }

        private void HandleResume()
        {
            TogglePauseMenu(false);
        }

        private void HandleSave()
        {
            if (MainGameManager.CurrentSave == null) return;
            SystemManager.SaveGame(MainGameManager.CurrentSave);
        }

        private void OnDisable()
        {
            save.onClick.RemoveAllListeners();
            backToMain.onClick.RemoveAllListeners();
            resume.onClick.RemoveAllListeners();
        }
    }
}
