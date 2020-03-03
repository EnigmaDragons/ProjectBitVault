using UnityEngine;

public sealed class StartDemoGameLevel : OnMessage<StartDemoLevelRequested, LevelCompleted>
{
    [SerializeField] private GameObject game;
    
    protected override void Execute(StartDemoLevelRequested msg)
    {
        Instantiate(game);
    }

    protected override void Execute(LevelCompleted msg)
    {
        DestroyImmediate(game);
    }
}
