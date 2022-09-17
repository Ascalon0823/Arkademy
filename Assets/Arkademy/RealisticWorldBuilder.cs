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

        #region Tectonic plate

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
            var voronoi = new Voronoi(points, bounds, 2);
            var dict = new Dictionary<Vector2f, int>();
            foreach (var site in voronoi.SitesIndexedByLocation.Values)
            {
                var plate = new World.TectonicPlate
                {
                    Origin = new Vector2Int(Mathf.FloorToInt(site.x), Mathf.FloorToInt(site.y)),
                    Density = Random.Range(0, 2),
                    Direction = ((SquareDir) (Random.Range(0, 4) * 2)).ToCoord()
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
                tile.Altitude = 0;
                tile.TectonicIdx = index;
                world[x, y] = tile;
            });

            world.Iterate((x, y) =>
            {
                var valideNeighbours = new Vector2Int(x, y).Neighbours()
                    .Where(coord => world.IsValid(coord.x, coord.y) && (coord.x == x || coord.y == y))
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
            foreach (var plate in world.TectonicPlates)
            {
                foreach (var edge in plate.Edges)
                {
                    var currTile = world[edge.x, edge.y];
                    currTile.EdgeType = GetEdgeType(edge, plate, world);
                    world[edge.x, edge.y] = currTile;
                }
            }
        }

        private World.TectonicPlate.EdgeType GetEdgeType(Vector2Int edge, World.TectonicPlate plate, World world)
        {
            var neighbourCoord = edge
                .Neighbours()
                .Where(coord => world.IsValid(coord.x, coord.y) && (coord.x == edge.x || coord.y == edge.y)
                                                                && world[coord.x, coord.y].TectonicIdx !=
                                                                world[edge.x, edge.y].TectonicIdx)
                .OrderByDescending(coord =>
                    Vector2.Dot(world.GetPlateByCoord(coord.x, coord.y).Origin - plate.Origin,
                        coord - edge))
                .First();
            var neighbour = world[neighbourCoord.x, neighbourCoord.y];
            var neighbourPlate = world.TectonicPlates[neighbour.TectonicIdx];
            var dot = Vector2.Dot(plate.Direction, neighbourPlate.Direction);
            var dir = neighbourCoord - edge;
            var dirDot = Vector2.Dot(plate.Direction, dir);
            var neighbourDirDot = Vector2.Dot(neighbourPlate.Direction, dir);
            if (dot > 0.5f) //Same dir
            {
                return World.TectonicPlate.EdgeType.Static;
            }

            if (dirDot < -0.5f) //Separate
            {
                return World.TectonicPlate.EdgeType.Divergent;
            }

            if (dirDot > 0.5f) //Collide
            {
                return neighbourPlate.Density == plate.Density
                    ? World.TectonicPlate.EdgeType.Collision
                    : World.TectonicPlate.EdgeType.Subduction;
            }

            if (neighbourDirDot < -0.5f) //neighbour incoming
            {
                return neighbourPlate.Density == plate.Density
                    ? World.TectonicPlate.EdgeType.Collision
                    : World.TectonicPlate.EdgeType.Subduction;
            }

            if (neighbourDirDot > 0.5f) //neighbour incoming
            {
                return World.TectonicPlate.EdgeType.Divergent;
            }

            return World.TectonicPlate.EdgeType.Static;
        }

        #endregion
    }
}