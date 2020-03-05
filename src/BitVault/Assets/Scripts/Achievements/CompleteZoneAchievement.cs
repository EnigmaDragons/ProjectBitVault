using System.Linq;
using UnityEngine;

public class CompleteZoneAchievement : OnMessage<StarsUpdated>
{
    [SerializeField] private Campaign mainCampaign;
    [SerializeField] private CurrentZone zone;
    [SerializeField] private int zoneNumber;
    [SerializeField] private SaveStorage storage;
    [SerializeField] private int minCubesPerLevel;
    [SerializeField] private Achievements achievements;
    [SerializeField] private StatType stat;
    [SerializeField] private AchievementType achievement;

    protected override void Execute(StarsUpdated msg)
    {
        if (mainCampaign != zone.Campaign)
            return;
        var progress = mainCampaign.Value[zoneNumber].Value.Count(x => storage.GetStars(x) >= minCubesPerLevel);
        achievements.SetStat(stat, progress);
        if (progress == 12)
            achievements.UnlockAchievement(achievement);
    }
}
