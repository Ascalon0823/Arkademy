using System;
using Arkademy.StageBuilders;
using Arkademy.TilePicker;
using UnityEngine;

namespace Arkademy
{
    public class StageBehaviour : MonoBehaviour
    {
        public static StageBehaviour Instance;
        private Stage currentStage;
        [SerializeField] private StageBuilder builder;
        private GameObject stageGO;
        [SerializeField] private StageTilePicker tilePicker;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void Start()
        {
            BuildStage();
        }

        private void BuildStage()
        {
            if (stageGO) Destroy(stageGO);
            stageGO = new GameObject("Stage");
            currentStage = builder.Build();
            currentStage.Iterate((x, y) =>
            {
                var go = tilePicker.GetTileObject(currentStage[x, y], currentStage);
                go.transform.SetParent(stageGO.transform);
                go.transform.position = currentStage.GetPos(x, y);
            });
        }
    }
}