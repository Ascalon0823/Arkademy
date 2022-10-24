using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Arkademy
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] spawnList;
        public void SpawnObjects()
        {
            for (var i = 0; i < spawnList.Length; i++)
            {
                Instantiate(spawnList[i], transform.position + (Vector3) (Mathf.Min(i, 1f) * Random.insideUnitCircle),
                    Quaternion.identity);
            }
        }
    }
}