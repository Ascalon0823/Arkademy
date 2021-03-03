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

        public static void NewGame()
        {
            SaveGame();
            // Debug.Log("[System] Start new game");
            // SceneManager.LoadScene(MainGameScene);
        }
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

            /** mock data **/
            Debug.Log("Load last saved time: " + save.SaveTime.ToString());
            Debug.Log("Load last saved character 1: " + save.Characters[0].Name);
            Debug.Log("Load last saved character 2: " + save.Characters[1].Name);
            /** mock data **/
        }

        public static void SaveGame()
        {
            /** mock data **/
            var user1 = new Character (1, "user1", new List<ISpell>{new FireBall()});
            var user2 = new Character (2, "AI", new List<ISpell>{new IceBeam()});
            var characters = new List<Character>{user1, user2}; 

            var save = new Save {
                Characters = characters,
                SaveTime = DateTime.Now
            };
            /** mock data **/

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
