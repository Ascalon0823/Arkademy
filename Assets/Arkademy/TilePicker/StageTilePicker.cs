using System;
using UnityEngine;

namespace Arkademy.TilePicker
{
    [CreateAssetMenu(menuName = "Tile Prefab Picker/Create Stage Tile Prefab Picker",
        fileName = "NewStageTilePicker")]
    public class StageTilePicker : ScriptableObject
    {
        [SerializeField] private GameObject wall;
        [SerializeField] private GameObject floor;
        public virtual GameObject GetTileObject(StageTile tile, Stage stage)
        {
            return tile.TileType switch
            {
                StageTile.Type.Void => new GameObject(),
                StageTile.Type.Wall => Instantiate(wall),
                StageTile.Type.Floor => Instantiate(floor),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}