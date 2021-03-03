using UnityEngine;

namespace Arkademy.UI.MainMenu
{
    public class UIMainMenu : MonoBehaviour
    {
        [SerializeField]private UICharacterCreationPage _characterCreationPage;
        public void HandleNewGameButton()
        {
            if (_characterCreationPage == null) return;
            _characterCreationPage.gameObject.SetActive(true);
        }
        public void HandleExitButton()
        {
            SystemManager.Exit();
        }
    }
}
