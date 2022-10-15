using UnityEngine;
using UnityEngine.UI;

namespace Arkademy.UI.Title
{
    public class TitleControl : MonoBehaviour
    {
        [SerializeField] private Button newGame;
        [SerializeField] private Button continu;
        private void Start()
        {
            continu.gameObject.SetActive(GameSystem.HasSave());
            continu.onClick.RemoveAllListeners();
            continu.onClick.AddListener(GameSystem.LoadGame);
            newGame.onClick.RemoveAllListeners();
            newGame.onClick.AddListener(GameSystem.NewGame);
        }
    }
}
