using System;
using UnityEngine;

namespace Arkademy.Game
{
    [RequireComponent(typeof(Caster))]
    public class PlayerCasterControl : MonoBehaviour
    {
        [SerializeField] private Caster caster;
        [SerializeField] private Caster.CastEvent lastCastEvent;
        private void Update()
        {
            var aimPlane = new Plane(caster.transform.position, Vector3.up);
            var cursorPos = PlayerCamera.Current.PointAtPos(null, aimPlane);
            //TODO: proper binding
            if (Input.GetMouseButtonDown(0))
            {
                lastCastEvent = new Caster.CastEvent
                {
                    caster = caster,
                    currPos = cursorPos,
                    originPos = PlayerCamera.Current.PointAtPos(),
                    currTarget = PlayerCamera.Current.PointAtObj(),
                    originTarget = PlayerCamera.Current.PointAtObj(),
                    state = Caster.CastState.Begin,
                    timePassed = 0f
                };
                caster.HandleCastEvent(lastCastEvent);
                return;
            }
            if (Input.GetMouseButtonUp(0))
            {
                lastCastEvent = new Caster.CastEvent
                {
                    caster = caster,
                    currPos = cursorPos,
                    originPos = lastCastEvent.originPos,
                    currTarget = PlayerCamera.Current.PointAtObj(),
                    originTarget = lastCastEvent.originTarget,
                    state = Caster.CastState.End,
                    timePassed = 0f
                };
                caster.HandleCastEvent(lastCastEvent);
                return;
            }
            if (Input.GetMouseButton(0))
            {
                lastCastEvent = new Caster.CastEvent
                {
                    caster = caster,
                    currPos = cursorPos,
                    originPos = lastCastEvent.originPos,
                    currTarget = PlayerCamera.Current.PointAtObj(),
                    originTarget = lastCastEvent.originTarget,
                    state = Caster.CastState.Casting,
                    timePassed = 0f
                };
                caster.HandleCastEvent(lastCastEvent);
                return;
            }
        }
    }
}
