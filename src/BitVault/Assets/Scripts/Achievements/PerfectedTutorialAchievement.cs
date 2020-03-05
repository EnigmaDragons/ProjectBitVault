using System.Linq;
using UnityEngine;

public class PerfectedTutorialAchievement : OnMessage<StarsUpdated>
{
    [SerializeField] private Campaign mainCampaign;
    [SerializeField] private CurrentZone zone;
    [SerializeField] private SaveStorage storage;
    [SerializeField] private Achievements achievements;

    protected override void Execute(StarsUpdated msg)
    {
        if (mainCampaign != zone.Campaign)
            return;
        var progress = mainCampaign.Value.Count(x => x.Tutorial.IsPresent && storage.GetStars(x.Tutorial.Value) == 3);
        achievements.SetStat(StatType.TutorialsPerfected, progress);
        if (progress == 4)
            achievements.UnlockAchievement(AchievementType.PerfectTutorial);
    }
}
