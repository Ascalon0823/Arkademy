using UnityEngine;

namespace Arkademy
{
    [CreateAssetMenu(menuName = "World Builder/Create World Builder",fileName = "NewWorldBuilder")]
    public class WorldBuilder : ScriptableObject
    {
        [SerializeField] protected int worldSeed;
        [SerializeField] protected bool randomWorldSeed;
        [SerializeField] protected int currWorldSeed;
        [SerializeField] protected int heightSeed;
        [SerializeField] protected bool randomHeightSeed;
        [SerializeField] protected int currHeightSeed;
        [SerializeField] protected int minHeight;
        [SerializeField] protected int maxHeight;
        public virtual World BuildWorld(int size)
        {
            return World.Create(size);
        }
    }
}
