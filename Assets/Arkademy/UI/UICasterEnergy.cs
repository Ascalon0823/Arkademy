using System;
using Arkademy.Game;
using UnityEngine;
using UnityEngine.UI;
namespace Arkademy.UI
{
    public class UICasterEnergy : MonoBehaviour
    {
        [SerializeField] private Caster caster;
        [SerializeField] private Image fill;

        private void Update()
        {
            if (!caster||!fill)
            {
                return;
            }

            fill.fillAmount = caster.energy / caster.maxEnergy;
        }
    }
}