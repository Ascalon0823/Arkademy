using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using UnityEngine.UI;
using Arkademy.Characters;
using System.Collections;
using System.Collections.Generic;
using Arkademy.Spells;
using System.Runtime.Serialization.Formatters.Binary;

namespace Arkademy.GameStatus 
{

    public static class SystemManager
    {
        private const string MainGameScene = "MainGame";
        private const string MainMenuScene = "MainMenu";

        public static void LoadGame()
        {
            if (!File.Exists(Application.persistentDataPath + "/gamesave.save")){
                Debug.Log("No save file found.");
                return;
            }

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();

            // todo: pass the loaded data to a struct

            Debug.Log("[System] Start game");
            SceneManager.LoadScene(MainGameScene);
        }

        public static void SaveGame(Save save)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
            bf.Serialize(file, save);
            file.Close();

            Debug.Log("Game Saved");
        }

        public static void GoToMainMenu()
        {
            Debug.Log("[System] Go to main menu");
            SceneManager.LoadScene(MainMenuScene);
        }

        public static void Exit()
        {
            Debug.Log("[System] Exit game");
            Application.Quit();
        }
    }
}
