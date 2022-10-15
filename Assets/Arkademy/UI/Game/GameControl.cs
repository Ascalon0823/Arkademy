using UnityEngine;
using UnityEngine.UI;

namespace Arkademy.UI.Game
{
    public class GameControl : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private Button pause;
        [SerializeField] private Button resume;
        [SerializeField] private Button saveExit;
        private void Start()
        {
            pauseMenu.SetActive(false);
            pause.onClick.RemoveAllListeners();
            pause.onClick.AddListener(() =>
            {
                pauseMenu.gameObject.SetActive(true);
                ApplicationManager.PauseGame();
            });
            
            resume.onClick.RemoveAllListeners();
            resume.onClick.AddListener(() =>
            {
                pauseMenu.gameObject.SetActive(false);
                ApplicationManager.UnpauseGame();
            });
            
            saveExit.onClick.RemoveAllListeners();
            saveExit.onClick.AddListener(() =>
            {
                ApplicationManager.UnpauseGame();
                GameSystem.SaveGame();
                GameSystem.ReturnToTitle();
            });
        }

    }
}
