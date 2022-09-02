using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Arkademy
{
    [CreateAssetMenu(menuName = "World Builder/Create Realistic World Builder", fileName = "NewRealisticWorldBuilder")]
    public class RealisticWorldBuilder : WorldBuilder
    {
        [Header("Tectonic Plate")] [SerializeField]
        protected int minAmountPlates;

        [SerializeField] protected int maxAmountPlates;

        public override World BuildWorld(int size)
        {
            var world = World.Create(size);
            Random.InitState((int) DateTime.UtcNow.Ticks);
            currWorldSeed = randomWorldSeed ? Random.Range(0, int.MaxValue) : worldSeed;
            Random.InitState(currWorldSeed);
            CreateTectonicPlates(world);
            return world;
        }

        protected void CreateTectonicPlates(World world)
        {
            world.TectonicPlates.Clear();
            var numPlates = Random.Range(minAmountPlates, Mathf.Max(minAmountPlates + 1, maxAmountPlates));
            for (var i = 0; i < numPlates; i++)
            {
                var x = Random.Range(0, world.Width());
                var y = Random.Range(0, world.Height());
                var plate = new World.TectonicPlate
                {
                    Origin = new Vector2Int(x, y)
                };
                world.TectonicPlates.Add(plate);
            }

            foreach (var plate in world.TectonicPlates)
            {
                var tile = world[plate.Origin.x, plate.Origin.y];
                tile.Altitude = 100;
                world[plate.Origin.x, plate.Origin.y] = tile;
            }
        }
    }
}