using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Arkademy.GameStatus;
using Arkademy.UI.MainGame;
using Arkademy.Player;
namespace Arkademy
{
    public class MainGameManager : MonoBehaviour
    {
        public static Save CurrentSave { get; private set;}
        public static void SetCurrentSave(Save save)
        {
            CurrentSave = save;
        }
        public static void ClearCurrentSave()
        {
            CurrentSave = null;
        }

        private void Awake()
        {
            InitGame();
        }

        private void InitGame()
        {
            if (CurrentSave == null)
            {
                Debug.Log("No game save loaded");
                return;
            }
            if (!SystemManager.InMainGame())
            {
                Debug.Log("Not in main game");
                return;
            }
            Debug.Log($"Initializing game with: {CurrentSave}");
            Player.Player.NewPlayer(CurrentSave);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //Todo: Implement proper game state handles pause
                UIPauseMenu.TogglePauseMenu();
            }
        }
    }

}
