using System;
using System.Collections;
using System.Collections.Generic;
using CGS.Grid;
using UnityEngine;

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
            foreach (var layer in tilePickerLayers)
            {
                var layerGo = new GameObject(layer.layer);
                layerGo.transform.SetParent(worldGo.transform);
                layerGo.transform.localPosition = Vector3.zero;
                currWorld.Iterate((x, y) =>
                {
                    var go = layer.tilePicker.GetTileObject(currWorld[x, y], currWorld);
                    if (go == null) return;
                    go.transform.position = currWorld.GetPos(x, y);
                    go.transform.SetParent(layerGo.transform);
                });
            }
        }
    }
}