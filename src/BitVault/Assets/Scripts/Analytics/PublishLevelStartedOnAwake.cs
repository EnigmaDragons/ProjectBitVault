using UnityEngine;
using UnityEngine.Analytics;

public class PublishLevelStartedOnAwake : MonoBehaviour
{
    [SerializeField] private CurrentLevel level;
    
    private void Awake() => AnalyticsEvent.LevelStart(level.ActiveLevel.Name);
}
