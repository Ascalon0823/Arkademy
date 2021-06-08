using System;
using UnityEngine;

namespace Arkademy.Game
{
    public enum SpellTargetType
    {
        Objects,
        Direction,
        Area
    }
    public class SpellBehaviour : MonoBehaviour
    {
       public SpellTargetType targetType;
    }
}