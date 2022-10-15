using UnityEngine;

namespace Arkademy.StageBuilders
{
    
    public abstract class StageBuilder : ScriptableObject
    {
        public abstract Stage Build();
    }
}
