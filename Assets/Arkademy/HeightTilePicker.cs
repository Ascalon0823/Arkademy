using UnityEngine;

namespace Arkademy
{
    [CreateAssetMenu(fileName = "NewHeightTilePicker", menuName = "Tile Prefab Picker/Create Height Tile Prefab Picker",
        order = 3)]
    public class HeightTilePicker : TilePrefabPicker
    {
        [SerializeField] protected SpriteRenderer basePrefab;
        [SerializeField] protected Color minColor;
        [SerializeField] protected Color maxColor;

        public override GameObject GetTileObject(WorldTile tile, World world)
        {
            var go = Instantiate(basePrefab);
            go.name = tile.Altitude.ToString();
            go.color = Color.LerpUnclamped(minColor, maxColor, tile.Altitude * 1.0f / 100f);
            return go.gameObject;
        }
    }
}