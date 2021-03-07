using System;
using Arkademy.Characters;
using System.Collections.Generic;
using UnityEngine;

namespace Arkademy.GameStatus
{

    [System.Serializable]
    public class Save {
        public List<Character> Characters = new List<Character>();
        public DateTime SaveTime;
        public override string ToString()
        {
            String result = "";
            result += "Characters: \n";
            foreach (var chara in Characters)
            {
                result += $"  {chara.ToString()}\n";
            }
            result += $"SaveTime: {SaveTime}";
            return result;
        }
    }

}