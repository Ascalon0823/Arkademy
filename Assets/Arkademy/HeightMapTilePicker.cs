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
    [CreateAssetMenu(menuName = "Tile Prefab Picker/Create Height Map Tile Prefab Picker", fileName = "NewHeightMapTilePicker")]
    public class HeightMapTilePicker : TilePrefabPicker
    {
        [SerializeField] protected RangeTileMapping[] tileMappings;

        public override GameObject GetTileObject(WorldTile tile)
        {
            var tileMapping = tileMappings.OrderBy(x => x.max);

            foreach (var mapping in tileMapping)
            {
                if (mapping.max >= tile.Altitude && mapping.tilePrefab)
                {
                    return Instantiate(mapping.tilePrefab);
                }
            }

            return null;
        }
    }
}
