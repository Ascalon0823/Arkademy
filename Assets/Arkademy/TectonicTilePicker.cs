using System;
using System.Linq;
using UnityEngine;

namespace Arkademy
{
    [CreateAssetMenu(menuName = "Tile Prefab Picker/Create Tectonic Tile Prefab Picker",
        fileName = "NewTectonicTilePrefabPicker")]
    public class TectonicTilePicker : TilePrefabPicker
    {
        [Serializable]
        public struct EdgeTypeObj
        {
            public World.TectonicPlate.EdgeType EdgeType;
            public GameObject Obj;
        }

        [SerializeField] protected GameObject oceanPlateTile;
        [SerializeField] protected GameObject groundPlateTile;
        [SerializeField] protected GameObject directionObj;
        [SerializeField] protected EdgeTypeObj[] oceanEdge;
        [SerializeField] protected EdgeTypeObj[] groundEdge;

        public override GameObject GetTileObject(WorldTile tile, World world)
        {
            var tileObj = Instantiate(world.TectonicPlates[tile.TectonicIdx].Density == 0
                ? oceanPlateTile
                : groundPlateTile);
            if (tile.TectonicEdge)
            {
                var dir = world.TectonicPlates[tile.TectonicIdx].Direction;
                var dirObj = Instantiate(directionObj, tileObj.transform, false);
                var dirArrow = dirObj.transform.GetChild(0);
                dirArrow.up = dir;
                var edgeObj = (world.TectonicPlates[tile.TectonicIdx].Density == 0
                    ? oceanEdge
                    : groundEdge)?.FirstOrDefault(x => x.EdgeType == tile.EdgeType);
                if (edgeObj.HasValue && edgeObj.Value.Obj != null)
                {
                    Instantiate(edgeObj.Value.Obj, tileObj.transform,false);
                }
                
            }

            return tileObj;
        }
    }
}