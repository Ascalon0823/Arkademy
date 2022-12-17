using System;
using Arkademy.AI;
using UnityEngine;

namespace Arkademy
{
    public class ActorSpawner : MonoBehaviour
    {
        public GameObject spawn;
        public float interval;
        public bool immediateSpawn;
        public int spawnedCount;
        [SerializeField] private float lastSpawn;

        void Update()
        {
            if (interval <= 0)
            {
                if (spawnedCount == 0) Spawn();
                return;
            }
            if (Time.timeSinceLevelLoad - lastSpawn < interval) return;
            if (lastSpawn != 0f || immediateSpawn)
            {
                Spawn();
            }
            lastSpawn = Time.timeSinceLevelLoad;
        }

        public void Spawn()
        {
            var pos = StageBehaviour.Instance.GetRandomPositionFrom(Player.LocalPlayer.currActor.transform.position, 5, 10);
            var actor = Instantiate(spawn, pos, Quaternion.identity);
            actor.GetComponent<AIFollowTarget>().target = Player.LocalPlayer.currActor.transform;
            spawnedCount++;
        }
    }
}
