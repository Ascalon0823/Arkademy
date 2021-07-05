using UnityEngine;

namespace Arkademy.Game
{
    [System.Serializable]
    public struct MapBuilder
    {
        [SerializeField] private int seed;
        [SerializeField] private bool generateNewSeed;
        [Range(0, 1)] [SerializeField] private float walkablePercent;

        [Range(1, 32)] [SerializeField] private float frequency;

        public void BuildMap(Map map)
        {
            if (generateNewSeed)
            {
                seed = Random.Range(int.MinValue, int.MaxValue);
            }

            Random.InitState(seed);
            var percent = walkablePercent;
            var freq = frequency;
            map.Iterate((i, j) =>
            {
                var mapCell = map[i, j];
                mapCell.walkable = Mathf.PerlinNoise(1.0f * i / map.Width() * freq
                    , 1.0f * j / map.Height() * freq) < percent;
                map[i, j] = mapCell;
            });
        }
    }
}