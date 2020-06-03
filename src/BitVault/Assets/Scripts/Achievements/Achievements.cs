#if UNITY_ANDROID || UNITY_IOS || UNITY_TIZEN || UNITY_TVOS || UNITY_WEBGL || UNITY_WSA || UNITY_PS4 || UNITY_WII || UNITY_XBOXONE || UNITY_SWITCH
#define DISABLESTEAMWORKS
#endif

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#if !DISABLESTEAMWORKS 
    using Steamworks;
#endif

public class Achievements : MonoBehaviour
{
#if !DISABLESTEAMWORKS 
    using Steamworks;

    private readonly Dictionary<AchievementType, string> _achievements = new Dictionary<AchievementType, string>
    {
        { AchievementType.BeatZone1, "ACH_BEAT_ZONE1" },
        { AchievementType.BeatZone2, "ACH_BEAT_ZONE2" },
        { AchievementType.BeatZone3, "ACH_BEAT_ZONE3" },
        { AchievementType.BeatZone4, "ACH_BEAT_ZONE4" },
        { AchievementType.BeatZone5, "ACH_BEAT_ZONE5" },
        { AchievementType.PerfectZone1, "ACH_PERFECT_ZONE1" },
        { AchievementType.PerfectZone2, "ACH_PERFECT_ZONE2" },
        { AchievementType.PerfectZone3, "ACH_PERFECT_ZONE3" },
        { AchievementType.PerfectZone4, "ACH_PERFECT_ZONE4" },
        { AchievementType.PerfectZone5, "ACH_PERFECT_ZONE5" },
        { AchievementType.PerfectTutorial, "ACH_PERFECT_TUTORIAL" },
        { AchievementType.AllRoutesLevel1, "ACH_ALL_ROUTES_LEVEL1" },
        { AchievementType.FailsafeGoal, "ACH_FAILSAFE_GOAL" },
        { AchievementType.Thinking, "ACH_THINKING" },
        { AchievementType.FailsafeEngaged, "ACH_FAILSAFE_ENGAGED" },
        { AchievementType.SpeedLevel, "ACH_SPEED_LEVEL" },
        { AchievementType.FailsafeUndo, "ACH_FAILSAFE_UNDO" },
        { AchievementType.LineUp, "ACH_LINE_UP" },
        { AchievementType.LaterLevel, "ACH_LATER_LEVEL" },
        { AchievementType.HackThePlanet, "ACH_ALL_ACHIEVEMENTS" },
    };

    private readonly Dictionary<StatType, string> _stats = new Dictionary<StatType, string>
    {
        {StatType.LevelsCompletedZone1, "LevelsCompletedZone1"},
        {StatType.LevelsCompletedZone2, "LevelsCompletedZone2"},
        {StatType.LevelsCompletedZone3, "LevelsCompletedZone3"},
        {StatType.LevelsCompletedZone4, "LevelsCompletedZone4"},
        {StatType.LevelsCompletedZone5, "LevelsCompletedZone5"},
        {StatType.LevelsPerfectedZone1, "LevelsPerfectedZone1"},
        {StatType.LevelsPerfectedZone2, "LevelsPerfectedZone2"},
        {StatType.LevelsPerfectedZone3, "LevelsPerfectedZone3"},
        {StatType.LevelsPerfectedZone4, "LevelsPerfectedZone4"},
        {StatType.LevelsPerfectedZone5, "LevelsPerfectedZone5"},
        {StatType.TutorialsPerfected, "TutorialsPerfected"},
        {StatType.DifferentPathsTakenLevel1, "DifferentPathsTakenLevel1"},
        {StatType.MaxFailsafesEngaged, "MaxFailsafesEngaged"},
    };
    private readonly DictionaryWithDefault<AchievementType, bool> _completedAchievements = new DictionaryWithDefault<AchievementType, bool>(false);
    private readonly DictionaryWithDefault<StatType, int> _statValues = new DictionaryWithDefault<StatType, int>(0);

    private CGameID _gameID;

    private void OnEnable()
    {
        if (!SteamManager.Initialized)
            return;

        _gameID = new CGameID(SteamUtils.GetAppID());
        Callback<UserStatsReceived_t>.Create(OnUserStatsReceived);
    }

    private void OnUserStatsReceived(UserStatsReceived_t pCallback)
    {
        if ((ulong)_gameID == pCallback.m_nGameID)
        {
            if (EResult.k_EResultOK == pCallback.m_eResult)
            {
                Debug.Log("Received stats and achievements from Steam\n");

                foreach (var achievement in _achievements)
                {
                    if (SteamUserStats.GetAchievement(achievement.Value, out var achieved))
                        _completedAchievements[achievement.Key] = achieved;
                    else
                        Debug.LogWarning("SteamUserStats.GetAchievement failed for Achievement " + achievement.Value);
                }

                foreach (var stat in _stats)
                {

                    if (SteamUserStats.GetStat(stat.Value, out int statValue))
                        _statValues[stat.Key] = statValue;
                    else
                        Debug.LogWarning("SteamUserStats.GetStat failed for Stat " + stat.Value);
                }

                if (_achievements.Count(x => _completedAchievements[x.Key]) == _achievements.Count - 1)
                    UnlockAchievement(AchievementType.HackThePlanet);
            }
            else
            {
                Debug.Log("RequestStats - failed, " + pCallback.m_eResult);
            }
        }
    }

    public void UnlockAchievement(AchievementType achievement)
    {
        if (!SteamManager.Initialized)
            return;

        if (!_completedAchievements[achievement])
        {
            var setAchievementSuccess = SteamUserStats.SetAchievement(_achievements[achievement]);
            if (setAchievementSuccess)
            {
                var storeStatsSuccess = SteamUserStats.StoreStats();
                if (storeStatsSuccess)
                    Debug.LogWarning("SteamUserStats.StoreStats failed");
            }
            else
            {
                Debug.LogWarning("SteamUserStats.SetAchievement failed for Achievement " + _achievements[achievement]);
            }
        }
    }

    public void SetStat(StatType stat, int value)
    {
        if (!SteamManager.Initialized)
            return;

        if (value > _statValues[stat])
        {
            var serStatSuccess = SteamUserStats.SetStat(_stats[stat], value);
            if (serStatSuccess)
            {
                var storeStatsSuccess = SteamUserStats.StoreStats();
                if (storeStatsSuccess)
                    Debug.LogWarning("SteamUserStats.StoreStats failed");
            }
            else
            {
                Debug.LogWarning("SteamUserStats.SetStat failed for Stat " + _stats[stat] + " with value of " + value);
            }
        }
    }
#else
    // NO-OP
    public void UnlockAchievement(AchievementType achievement)
    {
    }

    public void SetStat(StatType stat, int value)
    {
    }
#endif
}
