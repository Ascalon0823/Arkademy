using System;
using UnityEngine;

namespace Arkademy
{
    public class ApplicationManager : MonoBehaviour
    {
        public static bool Paused { get; private set; }

        public static void PauseGame()
        {
            Time.timeScale = 0f;
            Paused = true;
        }

        public static void UnpauseGame()
        {
            Time.timeScale = 1f;
            Paused = false;
        }

        private static ApplicationManager _instance;
    }
}