using UnityEngine;

namespace Arkademy.StageBuilders
{
    [CreateAssetMenu(menuName = "Stage Builder/Create Stage Builder", fileName = "NewStageBuilder")]
    public class StubStageBuilder : StageBuilder
    {
        [SerializeField] private int width;
        [SerializeField] private int height;

        public override Stage Build()
        {
            var stage = new Stage(width, height, new Vector3(-width / 2f, -height / 2f), new Vector3(1f, 1f, 0f));
            stage.Iterate((x, y) =>
            {
                var tile = stage[x, y];
                tile.TileType = stage.IsBoarder(x, y) ? StageTile.Type.Wall : StageTile.Type.Floor;
                stage[x, y] = tile;
            });
            return stage;
        }
    }
}