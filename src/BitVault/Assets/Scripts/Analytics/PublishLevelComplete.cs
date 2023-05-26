using UnityEngine;
using UnityEngine.Analytics;

public class PublishLevelComplete : OnMessage<LevelCompleted>
{
    [SerializeField] private CurrentLevel level;

    private bool _done;
    
    protected override void Execute(LevelCompleted msg)
    {
        if (!_done)
            AnalyticsEvent.LevelComplete(level.ActiveLevelName);
        _done = true;
    }
}
