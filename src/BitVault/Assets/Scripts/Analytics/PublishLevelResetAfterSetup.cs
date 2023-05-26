using UnityEngine;
using UnityEngine.Analytics;

public class PublishLevelResetAfterSetup : OnMessage<LevelReset>
{
    [SerializeField] private CurrentLevel level;

    private bool _isSetup;
    
    protected override void Execute(LevelReset msg)
    {
        if (_isSetup)
            AnalyticsEvent.LevelFail(level.ActiveLevelName);
        _isSetup = true;
    }
}
