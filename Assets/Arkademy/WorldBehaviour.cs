using System;
using System.Collections;
using System.Collections.Generic;
using Arkademy.TilePicker;
using CGS.Grid;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Arkademy
{
    [Serializable]
    public struct TilePickerLayers
    {
        public string layer;
        public TilePrefabPicker tilePicker;
    }

    public class WorldBehaviour : MonoBehaviour
    {
        public static WorldBehaviour Instance;
        [SerializeField] private int width;
        [SerializeField] private int height;

        [SerializeField] private WorldBuilder worldBuilder;
        [SerializeField] private bool built;
        [SerializeField] private bool createTile;
        [SerializeField] private TilePickerLayers[] tilePickerLayers;
        [SerializeField] private BaseTilePicker baseTilePicker;
        private Dictionary<string, GameObject> createdLayers = new Dictionary<string, GameObject>();
        private World currWorld;
        private GameObject worldGo;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void Start()
        {
            currWorld = worldBuilder.BuildWorld(width, height);
            built = true;
            if (createTile)
            {
                CreateTiles();
            }
        }

        private void CreateTiles()
        {
            if (worldGo)
            {
                Destroy(worldGo);
            }

            createdLayers.Clear();
            worldGo = new GameObject("World");
            worldGo.AddComponent<Grid>();
            var tileCount = currWorld.Width() * currWorld.Height();
            var layerGo = new GameObject();
            layerGo.transform.SetParent(worldGo.transform);
            layerGo.transform.localPosition = Vector3.zero;
            var tileMap = layerGo.AddComponent<Tilemap>();
            
            var poses = new Vector3Int[tileCount];
            var tiles = new TileBase[tileCount];
            var c = 0;
            currWorld.Iterate((x, y) =>
            {
                poses[c] = new Vector3Int(x, y, 0);
                tiles[c] = baseTilePicker.GetTileBaseAt(x, y, currWorld);
                c++;
            });
            tileMap.transform.localPosition = currWorld.AnchorPos;
            tileMap.SetTiles(poses, tiles);
            layerGo.gameObject.AddComponent<TilemapRenderer>();
        }
    }
}