using UnityEngine;

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
    }
}
