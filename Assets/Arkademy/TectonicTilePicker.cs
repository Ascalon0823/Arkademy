using UnityEngine;

namespace Arkademy
{
    [CreateAssetMenu(menuName = "Tile Prefab Picker/Create Tectonic Tile Prefab Picker", fileName = "NewTectonicTilePrefabPicker")]
    public class TectonicTilePicker : TilePrefabPicker
    {
        [SerializeField] protected GameObject ridgeTile;
        public override GameObject GetTileObject(WorldTile tile)
        {
            
            if (tile.TectonicEdge)
            {
                return Instantiate(ridgeTile);
            }

            return null;
        }
    }
}
