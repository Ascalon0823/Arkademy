using System;
using Arkademy.Characters;
using System.Collections.Generic;

namespace Arkademy.GameStatus
{

    [System.Serializable]
    public class Save {
        public List<Character> Characters = new List<Character>();
        public DateTime SaveTime;
    }

}