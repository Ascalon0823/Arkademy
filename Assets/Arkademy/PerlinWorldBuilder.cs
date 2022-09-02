using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Arkademy
{
    [CreateAssetMenu(menuName = "World Builder/Create Perlin World Builder", fileName = "NewPerlinWorldBuilder")]
    public class PerlinWorldBuilder : WorldBuilder
    {
        [SerializeField] protected int heightSeed;
        [SerializeField] protected bool randomHeightSeed;
        [SerializeField] protected int currHeightSeed;
        [SerializeField] protected int minHeight;
        [SerializeField] protected int maxHeight;
        [SerializeField] protected Vector2 perlinScale;
        [SerializeField] protected Vector2 perlinOffset;
        [SerializeField] protected bool randomOffset;

        public override World BuildWorld(int size)
        {
            var world = World.Create(size);
            Random.InitState((int) DateTime.UtcNow.Ticks);
            currWorldSeed = randomWorldSeed ? Random.Range(0, int.MaxValue) : worldSeed;
            Random.InitState(currWorldSeed);
            currHeightSeed = randomHeightSeed ? Random.Range(0, int.MaxValue) : heightSeed;
            Random.InitState(currHeightSeed);
            var offset = randomOffset ? Random.insideUnitCircle : perlinOffset;
            offset *= Random.Range(0, 1000);
            world.Iterate((x, y) =>
            {
                var tile = world[x, y];
                var sample = Mathf.PerlinNoise(
                                 (x * 1.0f / size + offset.x) * perlinScale.x,
                                 (y * 1.0f / size + offset.y) * perlinScale.y) *
                             (maxHeight - minHeight);
                tile.Altitude = Mathf.FloorToInt(sample) +
                                minHeight;
                world[x, y] = tile;
            });

            return world;
        }
    }
}