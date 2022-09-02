using UnityEngine;

namespace Arkademy
{
    [CreateAssetMenu(menuName = "World Builder/Create World Builder",fileName = "NewWorldBuilder")]
    public class WorldBuilder : ScriptableObject
    {
        [SerializeField] protected int worldSeed;
        [SerializeField] protected bool randomWorldSeed;
        [SerializeField] protected int currWorldSeed;
        
        public virtual World BuildWorld(int size)
        {
            return World.Create(size);
        }
    }
}
