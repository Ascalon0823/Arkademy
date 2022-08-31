using System;
using System.Collections;
using System.Collections.Generic;
using CGS.Grid;
using UnityEngine;

namespace Arkademy
{
    public class WorldBehaviour : MonoBehaviour
    {
        public static WorldBehaviour Instance;
        [SerializeField] private WorldBuilder worldBuilder;
        [SerializeField] private WorldTileObjectPicker tilePicker;
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
            currWorld = worldBuilder.BuildWorld(100);
            Build();
        }

        private void Build()
        {
            if (worldGo)
            {
                Destroy(worldGo);
            }

            worldGo = new GameObject();
            currWorld.Iterate((x, y) =>
            {
                var go = tilePicker.GetTileObject(currWorld[x, y]);
                go.transform.position = currWorld.GetPos(x, y);
                go.transform.SetParent(worldGo.transform);
            });
        }
    }
}