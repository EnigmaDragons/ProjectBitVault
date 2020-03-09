using System;
using System.Collections.Generic;

[Serializable]
public sealed class SavedGameData
{
    public string SaveDataVersion = "0.7.7";
    public int ActiveZone = 0;
    public string ActiveCampaignName = "";
    public CampaignsProgressData Campaigns = new CampaignsProgressData();
    public SettingsData Settings = new SettingsData();
    public bool HasWon = false;
    public AchievementData Achievements = new AchievementData();

    public CampaignLevelScores ActiveCampaign => Campaigns[ActiveCampaignName];
}

[Serializable]
public sealed class CampaignsProgressData : SerializableDictionary<string, CampaignLevelScores> {}

[Serializable]
public sealed class CampaignLevelScores : SerializableDictionary<string, int> {}

[Serializable]
public sealed class SettingsData
{
    public bool ShowMovementHints = true;
    public bool AutoSkipStory = false;
    public bool UseFemale = true;
}

[Serializable]
public sealed class AchievementData
{
    public List<List<TilePoint>> RoutesTakenOnLevel1 = new List<List<TilePoint>>();
}
