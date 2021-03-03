using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Arkademy.Pawns;
using Arkademy.Characters;

namespace Arkademy.Player {

    public class Player : MonoBehaviour
    {
        public static Player MainPlayer;
        public Pawn currentPawn;
        public Character currentCharacter;
        private void Awake()
        {
            MainPlayer = this;
        }

        private void Start()
        {
            currentPawn = GetComponentInChildren<Pawn>();
        }

        // Update is called once per frame
        private void Update()
        {
            
        }

        public void SetCharacter(Character character)
        {
            
        }
    }
}