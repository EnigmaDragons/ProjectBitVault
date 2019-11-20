using UnityEngine;
using UnityEngine.Analytics;

public class PublishLevelQuit : OnMessage<LevelCompleted>
{
    [SerializeField] private CurrentLevel level;
    
    private bool _levelFinished;
    
    protected override void Execute(LevelCompleted msg) => _levelFinished = true;

    private void OnDestroy()
    {
        if (!_levelFinished)
            AnalyticsEvent.LevelQuit(level.ActiveLevel.Name);
    }
}
