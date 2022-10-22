using System;
using UnityEngine;

namespace Arkademy.Graphics
{
    public class BoyanceTweenControl : MonoBehaviour
    {
        [SerializeField] private GameObject target;

        [SerializeField] private Vector3 startingLocalPos;
        [SerializeField] private float dropDistance;
        [SerializeField] private float dropTime;
        [SerializeField] private float distance;

        [SerializeField] private float time;

        private void Start()
        {
            target.transform.localPosition = startingLocalPos;
            LeanTween.moveLocalY(target, startingLocalPos.y + dropDistance, dropTime / 2f).setEaseOutQuad()
                .setLoopPingPong(1).setOnComplete(_ =>
                {
                    LeanTween.moveLocalY(target, startingLocalPos.y + distance, time).setEaseInOutSine()
                        .setLoopPingPong();
                });
            LeanTween.rotateZ(target, -1080f, dropTime);
        }
    }
}