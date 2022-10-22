using UnityEngine;

namespace Arkademy.Graphics
{
    public class ActionProgressControl : MonoBehaviour
    {
        [SerializeField] private ActionProgress progress;
        [SerializeField] private UnityEngine.UI.Image image;

        void Update()
        {
            if (!progress || !image) return;
            image.fillAmount = progress.currProgress < 1f ? progress.currProgress : 0f;
        }
    }
}