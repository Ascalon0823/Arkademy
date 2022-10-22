using System;
using UnityEngine;

namespace Arkademy.Graphics
{
    public class BoyanceTweenControl : MonoBehaviour
    {
        [SerializeField] private GameObject target;

        [SerializeField] private float distance;

        [SerializeField] private float time;

        private void Start()
        {
            LeanTween.moveLocalY(target, target.transform.localPosition.y + distance, time).setEaseInOutSine()
                .setLoopPingPong();
        }
    }
}