using UnityEngine;

public class SpeedLevelAchievement : OnMessage<EndingLevelAnimationFinished>
{
    [SerializeField] private CurrentLevelStars stars;
    [SerializeField] private Achievements achievements;

    private float _time = 0;

    private void Update() => _time += Time.deltaTime;

    protected override void Execute(EndingLevelAnimationFinished msg)
    {
        if (stars.Count == 3 && _time <= 30)
            achievements.UnlockAchievement(AchievementType.SpeedLevel);
    }
}
