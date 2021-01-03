using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Arkademy
{
    public class Highlighter : MonoBehaviour
    {
        private static Highlighter _instance;

        public static void HighlightOn(Interactable inter)
        {
            if (_instance == null)
            {
                CreateHighlighter();
            }

            _instance.Highlight(inter);
        }

        static void CreateHighlighter()
        {
            _instance = GameObject.CreatePrimitive(PrimitiveType.Sphere).AddComponent<Highlighter>();
            _instance.GetComponent<MeshRenderer>().shadowCastingMode = ShadowCastingMode.Off;
            _instance.transform.localScale = Vector3.one * 0.3f;
            _instance.gameObject.SetActive(false);
        }

        void Highlight(Interactable inter)
        {
            _instance.gameObject.SetActive(inter != null);
            if (inter == null) return;
            transform.position = inter.transform.position + Vector3.up * 2f;
        }
    }
}