using UnityEngine;

namespace Arkademy.Game
{
    [System.Serializable]
    public struct MapBuilder
    {
        [SerializeField] private int seed;
        [SerializeField] private bool generateNewSeed;
        [Range(0, 1)] [SerializeField] private float walkablePercent;
        [Header("Perlin setup")]
        [Range(1, 32)] [SerializeField] private float frequency;
        [Range(1, 10000)][SerializeField] private int offSetRange;

        public void BuildMap(Map map)
        {
            if (generateNewSeed)
            {
                seed = Random.Range(int.MinValue, int.MaxValue);
            }

            Random.InitState(seed);
            
            var noiseMap = CreatePerlinNoiseMap(map.Width(),map.Height());
            var percent = walkablePercent;
            map.Iterate((i, j) =>
            {
                var mapCell = map[i, j];
                mapCell.walkable = noiseMap[i,j]< percent;
                map[i, j] = mapCell;
            });
        }

        float[,] CreatePerlinNoiseMap(int x, int y)
        {
            var result = new float[x, y];
            var org = (Random.insideUnitCircle+Vector2.one)/2*offSetRange;
            for (var i = 0; i < x; i++)
            {
                for (var j = 0; j < y; j++)
                {
                    result[i,j] = Mathf.PerlinNoise(org.x + 1.0f * i / x * frequency
                        , org.y + 1.0f * j / y * frequency);
                }
            }
            return result;
        }
    }
}