using System;
using System.Linq;
using UnityEngine;

namespace Arkademy
{
    [Serializable]
    public struct RangeTileMapping
    {
        public float max;
        public GameObject tilePrefab;
    }

    [CreateAssetMenu(menuName = "Tile Object Picker/Create Tile Object Picker", fileName = "NewWorldTileObjectPicker")]
    public class WorldTileObjectPicker : ScriptableObject
    {
        [SerializeField] protected GameObject fallbackGameObject;
        [SerializeField] protected RangeTileMapping[] tileMappings;

        public virtual GameObject GetTileObject(WorldTile tile)
        {
            var tileMapping = tileMappings.OrderBy(x => x.max);

            foreach (var mapping in tileMapping)
            {
                if (mapping.max >= tile.Altitude && mapping.tilePrefab)
                {
                    return Instantiate(mapping.tilePrefab);
                }
            }

            var go = new GameObject();
            go.name = tile.Altitude.ToString();
            return go;
        }
    }
}