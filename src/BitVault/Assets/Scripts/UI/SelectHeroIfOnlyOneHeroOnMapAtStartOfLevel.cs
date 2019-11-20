using UnityEngine;

public class SelectHeroIfOnlyOneHeroOnMapAtStartOfLevel : OnMessage<LevelReset>
{
    [SerializeField] private CurrentLevelMap map;
    private bool _initialized;

    private void Start() => Execute();
    protected override void Execute(LevelReset msg) => Execute();
    private void Execute()
    {
        Message.Publish(new PieceSelected(map.Hero));
    }
}
