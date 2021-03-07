using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
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
            MainGameManager.SetCurrentSave(save);
            SceneManager.LoadScene(MainGameScene);
        }
        public static void SaveGame(Save save)
        {
            BinaryFormatter bf = new BinaryFormatter();
            save.SaveTime = DateTime.Now;
            FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
            bf.Serialize(file, save);
            file.Close();

            Debug.Log("Game Saved");
        }

        public static void GoToMainMenu()
        {
            Debug.Log("[System] Go to main menu");
            MainGameManager.ClearCurrentSave();
            SceneManager.LoadScene(MainMenuScene);
        }

        public static bool InMainMenu()
        {
            return SceneManager.GetActiveScene().name == MainMenuScene;
        }
        public static bool InMainGame()
        {
            return SceneManager.GetActiveScene().name == MainGameScene;
        }

        public static void Exit()
        {
            Debug.Log("[System] Exit game");
            Application.Quit();
        }
    }
}
