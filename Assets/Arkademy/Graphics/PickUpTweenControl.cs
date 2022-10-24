using System;
using UnityEngine;

namespace Arkademy.Graphics
{
    public class PickUpTweenControl : MonoBehaviour
    {
        [SerializeField] private PickUp pickUp;
        [SerializeField] private GameObject target;

        [SerializeField] private Vector3 startingLocalPos;
        [SerializeField] private float dropDistance;
        [SerializeField] private float distance;
        [SerializeField] private float time;

        private void Start()
        {
            pickUp = GetComponentInParent<PickUp>();
            if (!pickUp) return;
            target.transform.localPosition = startingLocalPos;
            LeanTween.moveLocalY(target, startingLocalPos.y + dropDistance, pickUp.dropTime / 2f).setEaseOutQuad()
                .setLoopPingPong(1).setOnComplete(_ =>
                {
                    LeanTween.moveLocalY(target, startingLocalPos.y + distance, time).setEaseInOutSine()
                        .setLoopPingPong();
                });
            LeanTween.rotateZ(target, -1080f, pickUp.dropTime);
        }
    }
}