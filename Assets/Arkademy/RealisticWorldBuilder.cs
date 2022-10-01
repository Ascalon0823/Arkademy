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
        [SerializeField] protected bool treatBoarderAsEdge;
        [Header("Height")] [SerializeField] protected int oceanBaseHeight;
        [SerializeField] protected int groundBaseHeight;
        [SerializeField] protected int edgeHeightRandomRange;
        [SerializeField] protected int collisionDiff;
        [SerializeField] protected int oceanSubductionDiff;
        [SerializeField] protected int groundSubductionDiff;
        [SerializeField] protected int oceanDivergentDiff;
        [SerializeField] protected int groundDivergentDiff;
        [SerializeField] protected int lerpHeightRandomRange;
        [SerializeField] protected float lerpStep;

        public override World BuildWorld(int sizeX, int sizeY)
        {
            var world = World.Create(sizeX, sizeY);
            Random.InitState((int) DateTime.UtcNow.Ticks);
            currWorldSeed = randomWorldSeed ? Random.Range(0, int.MaxValue) : worldSeed;
            Random.InitState(currWorldSeed);
            CreateTectonicPlates(world);
            CreateHeightMap(world);
            return world;
        }

        #region Tectonic plate

        protected void CreateTectonicPlates(World world)
        {
            world.TectonicPlates.Clear();
            var dict = CreatePlates(world);
            FillPlatesRandom(world);
            FindEdges(world);
        }

        private void ResetTilePlateIdx(World world)
        {
            world.IterateAndUpdate(
                (x, y, t) =>
                {
                    t.TectonicIdx = -1;
                    return t;
                });
        }


        private Dictionary<Vector2f, int> CreatePlates(World world)
        {
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

            return dict;
        }

        private void FillPlatesRandom(World world)
        {
            ResetTilePlateIdx(world);
            var remaining = world.Width() * world.Height();

            var expandable = new Dictionary<int, List<Vector2Int>>();
            while (remaining > 0)
            {
                var idx = Random.Range(0, world.TectonicPlates.Count);
                if (!expandable.TryGetValue(idx, out var set))
                {
                    expandable[idx] = new List<Vector2Int> {world.TectonicPlates[idx].Origin};
                }

                if (expandable[idx].Count == 0)
                {
                    expandable.Remove(idx);
                    continue;
                }

                var targetIdx = Random.Range(0, expandable[idx].Count);
                var targetCoord = expandable[idx][targetIdx];
                expandable[idx].RemoveAt(targetIdx);
                var tile = world[targetCoord.x, targetCoord.y];
                if (tile.TectonicIdx != -1)
                {
                    continue;
                }

                tile.TectonicIdx = idx;
                tile.Altitude = world.TectonicPlates[idx].Density == 0 ? oceanBaseHeight : groundBaseHeight;
                world[targetCoord.x, targetCoord.y] = tile;
                remaining--;

                var valideNeighbours = targetCoord.Neighbours()
                    .Where(coord =>
                        world.IsValid(coord.x, coord.y)
                        && world[coord.x, coord.y].TectonicIdx == -1
                        && (coord.x == targetCoord.x || coord.y == targetCoord.y));
                expandable[idx].AddRange(valideNeighbours);
            }
        }

        private void FillPlates(World world, Dictionary<Vector2f, int> dict)
        {
            ResetTilePlateIdx(world);
            var dist = new Func<Vector2f, int, int, float>((Vector2f v, int x, int y) => Vector2.Distance(
                new Vector2(x, y),
                new Vector2(v.x, v.y)
            ));


            world.Iterate((x, y) => //Fill
            {
                var tile = world[x, y];
                var index = dict.Aggregate((min, next) =>
                    dist(min.Key, x, y) < dist(next.Key, x, y) ? min : next
                ).Value;
                tile.Altitude = 0;
                tile.TectonicIdx = index;
                world[x, y] = tile;
            });
        }

        private void FindEdges(World world)
        {
            world.IterateAndUpdate((x, y, curr) =>
            {
                var valideNeighbours = new Vector2Int(x, y).Neighbours()
                    .Where(coord => world.IsValid(coord.x, coord.y) && (coord.x == x || coord.y == y))
                    .Select(coord => world[coord.x, coord.y]);
                if (valideNeighbours.Any(neighbour =>
                        neighbour.TectonicIdx != curr.TectonicIdx && curr.TectonicIdx != -1) ||
                    treatBoarderAsEdge && world.IsBoarder(x, y))
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

                return curr;
            });
            foreach (var plate in world.TectonicPlates)
            {
                foreach (var edge in plate.Edges)
                {
                    world.UpdateTile(edge, (currTile, _) =>
                    {
                        currTile.EdgeType = GetEdgeType(edge, plate, world);
                        currTile.Altitude += GetAltitudeChangeByEdgeType(currTile.EdgeType, plate.Density);
                        return currTile;
                    });
                }
            }
        }


        private World.TectonicPlate.EdgeType GetEdgeType(Vector2Int edge, World.TectonicPlate plate, World world)
        {
            var neighbourCoords = edge
                .Neighbours()
                .Where(coord => world.IsValid(coord.x, coord.y) && (coord.x == edge.x || coord.y == edge.y)
                                                                && world[coord.x, coord.y].TectonicIdx !=
                                                                world[edge.x, edge.y].TectonicIdx).ToList();
            if (neighbourCoords.Count == 0)
            {
                if (!treatBoarderAsEdge)
                {
                    return World.TectonicPlate.EdgeType.Static;
                }

                return plate.Density == 0
                    ? World.TectonicPlate.EdgeType.Subduction
                    : World.TectonicPlate.EdgeType.Collision;
            }

            var neighbourCoord = neighbourCoords
                .OrderByDescending(coord =>
                    Vector2.Dot(world.GetPlateByCoord(coord.x, coord.y).Origin - plate.Origin, coord - edge)
                )
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

        #region Height

        private void CreateHeightMap(World world)
        {
            foreach (var plate in world.TectonicPlates)
            {
                var queue = new List<Vector2Int>();
                queue.AddRange(plate.Edges);
                var processed = new HashSet<Vector2Int>();
                while (queue.Count > 0)
                {
                    var idx = Random.Range(0, queue.Count);
                    var coord = queue[idx];
                    processed.Add(coord);
                    queue.RemoveAt(idx);
                    var tile = world[coord.x, coord.y];
                    var neighbour = coord.Neighbours().Where(nei =>
                            world.IsValid(nei.x, nei.y)
                            && world[nei.x, nei.y].TectonicIdx == tile.TectonicIdx //Same plate
                            //&& !world[nei.x, nei.y].TectonicEdge
                            && (nei.x == coord.x || nei.y == coord.y) //4 dir
                            && !processed.Contains(nei) //processed
                        //&& Mathf.Abs(world[nei.x, nei.y].Altitude - tile.Altitude) > 3f
                    ).ToArray(); //Big diff
                    var count = neighbour.Length;
                    foreach (var nei in neighbour.OrderByDescending(x => Random.Range(0, count)))
                    {
                        world.UpdateTile(nei, (neiTile, _) =>
                            {
                                neiTile.Altitude =
                                    Mathf.FloorToInt(lerpStep * neiTile.Altitude + (1 - lerpStep) * tile.Altitude) +
                                    Random.Range(-lerpHeightRandomRange, lerpHeightRandomRange);
                                return neiTile;
                            }
                        );
                        queue.Add(nei);
                        processed.Add(nei);
                    }
                }
            }
        }

        private int GetAltitudeChangeByEdgeType(World.TectonicPlate.EdgeType type, int density)
        {
            return type switch
            {
                World.TectonicPlate.EdgeType.None => 0,
                World.TectonicPlate.EdgeType.Collision => collisionDiff +
                                                          Random.Range(-edgeHeightRandomRange, edgeHeightRandomRange),
                World.TectonicPlate.EdgeType.Subduction => (density == 0 ? oceanSubductionDiff : groundSubductionDiff) +
                                                           Random.Range(-edgeHeightRandomRange, edgeHeightRandomRange),
                World.TectonicPlate.EdgeType.Divergent => (density == 0 ? oceanDivergentDiff : groundDivergentDiff) +
                                                          Random.Range(-edgeHeightRandomRange, edgeHeightRandomRange),
                World.TectonicPlate.EdgeType.Shear => 0 + Random.Range(-edgeHeightRandomRange, edgeHeightRandomRange),
                World.TectonicPlate.EdgeType.Static => 0,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        #endregion
    }
}