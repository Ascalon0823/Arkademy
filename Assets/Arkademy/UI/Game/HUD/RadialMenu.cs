using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Arkademy.UI.Game.HUD
{
    public class RadialMenu : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        [SerializeField] private GameObject menu;

        [SerializeField] private RectTransform toggle;
        [SerializeField] private List<RectTransform> options = new List<RectTransform>();

        [SerializeField] private RectTransform currOption;

        // Start is called before the first frame update
        void Start()
        {
            menu.SetActive(false);
            options = menu.GetComponentsInChildren<RectTransform>().ToList();
            options.Remove(menu.GetComponent<RectTransform>());
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            menu.SetActive(true);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Debug.Log(currOption, currOption);
            menu.SetActive(false);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!menu.activeInHierarchy) return;
            var allOptions = new List<RectTransform>();
            allOptions.AddRange(options);
            allOptions.Add(toggle);
            var nearest = allOptions.OrderBy(x => Vector2.Distance(eventData.position, x.position))
                .First();
            var option = nearest == toggle ? null : nearest;
            if (currOption == option) return;
            if (currOption)
            {
                LeanTween.scale(currOption.gameObject, Vector3.one, 0.1f).setEaseOutQuad();
            }

            if (option)
            {
                LeanTween.scale(option.gameObject, Vector3.one * 1.5f, 0.1f).setEaseInQuad();
            }

            currOption = option;
        }
    }
}