using System;
using Arkademy.StageBuilders;
using Arkademy.TilePicker;
using UnityEngine;
using Random = UnityEngine.Random;

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
                go.layer = LayerMask.NameToLayer("Stage");
            });
        }

        public Vector3 GetRandomPositionFrom(Vector3 center, float from, float to)
        {
            from = Mathf.Max(0, from);
            to = Mathf.Min(new Vector2(currentStage.Width(), currentStage.Height()).magnitude, to);
            var radius = Random.Range(from, to);
            var p = (Vector3) Random.insideUnitCircle.normalized * radius + center;
            var coord = currentStage.FromPos(p);
            if (currentStage.IsValid(coord) && !currentStage.IsBoarder(coord.x, coord.y))
                return p;
            return GetRandomPositionFrom(center, from, to);
        }
    }
}