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
            var org = (Random.insideUnitCircle+Vector2.one)/2*Random.Range(0,10000);
            map.Iterate((i, j) =>
            {
                var mapCell = map[i, j];
                mapCell.walkable = Mathf.PerlinNoise( org.x + 1.0f * i / map.Width() * freq
                    ,  org.y + 1.0f * j / map.Height() * freq) < percent;
                map[i, j] = mapCell;
            });
        }
    }
}