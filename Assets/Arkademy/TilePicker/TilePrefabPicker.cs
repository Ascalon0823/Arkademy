using System;
using System.Linq;
using UnityEngine;

namespace Arkademy
{
    public abstract class TilePrefabPicker : ScriptableObject
    {
        public abstract GameObject GetTileObject(WorldTile tile, World world);
    }
}