using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CGS.Grid;
using UnityEngine;
using Random = UnityEngine.Random;
using csDelaunay;

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
            var points = new List<Vector2f>();
            for (var i = 0; i < numPlates; i++)
            {
                points.Add(
                    new Vector2f(
                        Random.Range(0, world.Width()),
                        Random.Range(0, world.Height())
                    )
                );
            }

            var bounds = new Rectf(0, 0, world.Width(), world.Height());
            var voronoi = new Voronoi(points, bounds, 5);
            var dict = new Dictionary<Vector2f, int>();
            foreach (var site in voronoi.SitesIndexedByLocation.Values)
            {
                var plate = new World.TectonicPlate
                {
                    Origin = new Vector2Int(Mathf.FloorToInt(site.x), Mathf.FloorToInt(site.y)),
                    Altitude = Random.Range(0, 100)
                };
                world.TectonicPlates.Add(plate);
                dict[site.Coord] = world.TectonicPlates.Count - 1;
            }

            var dist = new Func<Vector2f, int, int, float>((Vector2f v, int x, int y) => Vector2.Distance(
                new Vector2(x, y),
                new Vector2(v.x, v.y)
            ));

            world.Iterate((x, y) =>
            {
                var tile = world[x, y];
                var index = dict.Aggregate((min, next) =>
                    dist(min.Key, x, y) < dist(next.Key, x, y) ? min : next
                ).Value;
                tile.Altitude = world.TectonicPlates[index].Altitude;
                tile.TectonicIdx = index;
                world[x, y] = tile;
            });

            world.Iterate((x, y) =>
            {
                var valideNeighbours = new Vector2Int(x, y).Neighbours()
                    .Where(coord => world.IsValid(coord.x, coord.y))
                    .Select(coord => world[coord.x, coord.y]);
                var curr = world[x, y];
                if (valideNeighbours.Any(neighbour => neighbour.TectonicIdx != curr.TectonicIdx))
                {
                    curr.TectonicEdge = true;
                    var plate = world.TectonicPlates[curr.TectonicIdx];
                    plate.Edges ??= new List<Vector2Int>();
                    world.TectonicPlates[curr.TectonicIdx] = plate;
                    plate.Edges.Add(new Vector2Int(x, y));
                    
                }
                else
                {
                    curr.TectonicEdge = false;
                }

                world[x, y] = curr;
            });
        }
    }
}