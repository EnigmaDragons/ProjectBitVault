using UnityEngine;

public class StartLaterLevelAchievement : MonoBehaviour
{
    [SerializeField] private SaveStorage storage;
    [SerializeField] private CurrentZone zone;
    [SerializeField] private CurrentLevel level;
    [SerializeField] private Achievements achievements;

    private void Start()
    {
        if (level.LevelNumber > 0 && storage.GetStars(zone.Zone.Value[level.LevelNumber - 1]) == 0)
            achievements.UnlockAchievement(AchievementType.LaterLevel);
    }
}
