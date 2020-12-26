using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Arkademy{

    public class Player : MonoBehaviour
    {
        public static Player MainPlayer;
        public Pawn currentPawn;
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
    }
}