using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Arkademy
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] droptable;

        public void SpawnObjects()
        {
            if (droptable == null || droptable.Length == 0) return;
            var i = Random.Range(0, droptable.Length);

            Instantiate(droptable[i], transform.position + (Vector3) (Mathf.Min(i, 1f) * Random.insideUnitCircle),
                Quaternion.identity);
        }
    }
}