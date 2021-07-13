using UnityEngine;

namespace Arkademy.Game
{
    [CreateAssetMenu(fileName = "Spell Object", menuName = "Create Spell Object", order = 0)]
    public class SpellObject : ScriptableObject
    {
        public int minEnergyCost;
        
    }
}