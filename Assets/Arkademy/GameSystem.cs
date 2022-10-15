using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Arkademy
{
    public class GameSystem : MonoBehaviour
    {
        private static readonly string SAVE_FILE_NAME = "sav";
        private static string GET_SAVE_FILE_PATH => Path.Combine(Application.persistentDataPath, SAVE_FILE_NAME);
        private static bool IS_NEW_GAME;

        public static void SaveGame()
        {
            File.WriteAllText(GET_SAVE_FILE_PATH, "");
        }

        public static bool HasSave()
        {
            return File.Exists(GET_SAVE_FILE_PATH);
        }

        public static void LoadGame()
        {
            if (!HasSave()) return;
            IS_NEW_GAME = false;
            GoToGameScene();
        }

        public static void NewGame()
        {
            IS_NEW_GAME = true;
            GoToGameScene();
        }

        public static void ReturnToTitle()
        {
            SceneManager.LoadScene("Title");
        }

        public static void GoToGameScene()
        {
            SceneManager.LoadScene("Game");
        }
    }
}