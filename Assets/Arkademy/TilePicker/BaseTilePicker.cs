using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
namespace Arkademy.TilePicker
{
    [CreateAssetMenu(menuName = "Tile Picker/Create Tile Picker",
        fileName = "NewTilePicker")]
    public class BaseTilePicker : ScriptableObject
    {
        public List<Tile> tiles;
        [SerializeField] protected Color minColor;
        [SerializeField] protected Color maxColor;
        public TileBase GetTileBaseAt(int x, int y , World world)
        {
            var newTile = Instantiate(tiles[0]);
            newTile.color = Color.LerpUnclamped(minColor, maxColor, world[x,y].Altitude * 1.0f / 100f);
            return newTile;
        }
    }
}
