using System;
using System.Linq;
using UnityEngine;

namespace Arkademy
{
    // [Serializable]
    // public struct RangeTileMapping
    // {
    //     public float max;
    //     public GameObject tilePrefab;
    // }

    [CreateAssetMenu(menuName = "Tile Prefab Picker/Create Biome Tile Prefab Picker",
        fileName = "NewBiomeTilePicker")]
    public class BiomeTilePicker : TilePrefabPicker
    {
        //[SerializeField] protected RangeTileMapping[] tileMappings;

        public override GameObject GetTileObject(WorldTile tile, World world)
        {
            // var tileMapping = tileMappings.OrderBy(x => x.max).ToList();
            //
            // foreach (var mapping in tileMapping)
            // {
            //     if (mapping.max >= tile.Altitude && mapping.tilePrefab)
            //     {
            //         var go = Instantiate(mapping.tilePrefab);
            //         go.name = tile.Altitude.ToString();
            //         return go;
            //     }
            // }
            // var fallback = Instantiate(tileMapping.Last().tilePrefab);
            // fallback.name = tile.Altitude.ToString();
            // return fallback;
            return null;
        }
    }
}