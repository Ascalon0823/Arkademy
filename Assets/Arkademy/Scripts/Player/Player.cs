using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Arkademy.Pawns;
using Arkademy.Characters;
using Arkademy.GameStatus;
namespace Arkademy.Player {

    public class Player : MonoBehaviour
    {
        public static Player MainPlayer { get; private set; }
        public Pawn currentPawn;
        public Character currentCharacter;

        public static void NewPlayer(Save save)
        {
            if (MainPlayer != null)
            {
                Destroy(MainPlayer);
            }
            MainPlayer = Instantiate(Resources.Load<Player>("Player"));
            MainPlayer.InitPlayer(save);
        }

        private void InitPlayer(Save save)
        {
            Debug.Log("Initializing player");
        }
    }
}