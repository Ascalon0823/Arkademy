using UnityEngine;
using Arkademy.GameStatus;

namespace Arkademy.UI.MainMenu
{
    public class UIMainMenu : MonoBehaviour
    {
        public void HandleNewGameButton()
        {
            SystemManager.NewGame();
        }
        public void HandleExitButton()
        {
            SystemManager.Exit();
        }
        public void HandleLoadButton(){
            SystemManager.LoadGame();
        }
    }
}
