using UnityEngine;
using UnityEngine.SceneManagement;

namespace Arkademy
{
    public static class SystemManager
    {
        private const string MainGameScene = "MainGame";
        private const string MainMenuScene = "MainMenu";

        public static void NewGame()
        {
            Debug.Log("[System] Start new game");
            SceneManager.LoadScene(MainGameScene);
        }
        public static void LoadGame()
        {
            
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
