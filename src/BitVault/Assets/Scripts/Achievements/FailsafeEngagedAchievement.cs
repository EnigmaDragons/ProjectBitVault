using UnityEngine;

public class FailsafeEngagedAchievement : OnMessage<LevelStateChanged>
{
    [SerializeField] private CurrentLevelMap map;
    [SerializeField] private Achievements achievements;

    protected override void Execute(LevelStateChanged msg)
    {
        achievements.SetStat(StatType.MaxFailsafesEngaged, map.CountDangerousTiles());
        if (map.CountDangerousTiles() >= 18)
            achievements.UnlockAchievement(AchievementType.FailsafeEngaged);
    }
}
