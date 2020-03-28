using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;

[CreateAssetMenu]
public class SaveStorage : ScriptableObject
{
    [SerializeField] private CurrentZone current; 
    
    private const string _versionKey = "Version";
    private const string _version = "0.2";
    private const string _showMovementHints = "ShowMovementHints";
    private const string _autoSkipStory = "AutoSkipStory";
    private const string _useFemale = "UseFemale";
    private const string _defaultCampaignKey = "Main";
    
    private PlayerPrefsKeyValueStore _store = new PlayerPrefsKeyValueStore();
    private Stored<SavedGameData> _currentSave;
    
    public SavedGameData SaveData
    {
        get 
        {
            if (_currentSave == null)
                Init();
            return _currentSave.Get(); 
        }
    }

    private Campaign ActiveCampaign => current.Campaign;
    
    public void Init()
    {
        if (_store == null)
            _store = new PlayerPrefsKeyValueStore();
        if (_currentSave == null)
           _currentSave = new JsonFileStored<SavedGameData>(Path.Combine(Application.persistentDataPath, "Save.json"), () => new SavedGameData
            {
                ActiveCampaignName = _defaultCampaignKey,
                Campaigns = new CampaignsProgressData { { _defaultCampaignKey, new CampaignLevelScores()} }
            });
        _store.Put(_versionKey, _version);
    }
    
    public void Reset()
    {
        var showMovementHints = GetShowMovementHints();
        var skipStory = GetAutoSkipStory();
        _currentSave.Write(s =>
        {
            s.Campaigns = new CampaignsProgressData {{_defaultCampaignKey, new CampaignLevelScores()}};
            s.ZonesVisited = new List<int>();
            s.ActiveZone = 0;
        });
        _store.Clear();
        SetShowMovementHints(showMovementHints);
        SetAutoSkipStory(skipStory);
    }
    
    public Campaign GetCampaign() => ActiveCampaign;
    public void SetCampaign(Campaign activeCampaign)
    {
        _currentSave.Write(s => s.ActiveCampaignName = activeCampaign.Name);
        current.Init(activeCampaign);
    }
    
    // Player Save Data
    public int GetLevelsCompletedInZone(GameLevels zone) => zone.Value.Count(level => GetStars(level) > 0);
    public int GetZone() => SaveData.ActiveZone;
    public void SaveZone(int zone) => _currentSave.Write(s => s.ActiveZone = zone);
    public bool HasVisited(int zone) => SaveData.ZonesVisited.Contains(zone);
    public void Visit(int zone) => _currentSave.Write(s => s.ZonesVisited.Add(zone));
    public bool HasWon() => SaveData.HasWon;
    public void SaveWin() => _currentSave.Write(x => x.HasWon = true);
    public int GetTotalStars() => CampaignScores().Sum(x => x.Value);
    public int GetStars(GameLevel level) => CampaignScores().ValueOrDefault(level.Id, () => 0);
    public void SaveStars(GameLevel level, int stars)
    {
        if (GetStars(level) < stars) 
            _currentSave.Write(s => CampaignScores()[level.Id] = stars);
    }

    private CampaignLevelScores CampaignScores()
    {
        if (!SaveData.Campaigns.ContainsKey(ActiveCampaign.Name))
            _currentSave.Write(s => s.Campaigns[ActiveCampaign.Name] = new CampaignLevelScores());
        return SaveData.Campaigns[ActiveCampaign.Name];
    }

    // Settings
    public bool GetShowMovementHints() => _store.GetOrDefault(_showMovementHints, true);
    public void SetShowMovementHints(bool active) => _store.Put(_showMovementHints, active);

    public bool GetAutoSkipStory() => _store.GetOrDefault(_autoSkipStory, false);
    public void SetAutoSkipStory(bool active) => _store.Put(_autoSkipStory, active);

    public bool HasChosenGender() => _store.Exists(_useFemale);
    public bool GetUseFemale() => _store.GetOrDefault(_useFemale, false);
    public void SetUseFemale(bool active) => _store.Put(_useFemale, active);

    // Hints
    private const string _useHints = "UseHints";
    private const string _hintPoints = "HintPoints";
    private const string _dailyBonusDate = "DailyBonusDate";
    private const string _minutesTilHintBonus = "MinutesTilHintBonus";
    private string HintsKey(GameLevel level) => $"{level.Name}Hints";
    public int GetHints(GameLevel level) => _store.GetOrDefault(HintsKey(level), 0);
    public void AddHintToLevel(GameLevel level) => _store.Put(HintsKey(level), GetHints(level) + 1);
    public void ClearHints(GameLevel level) => _store.Put(HintsKey(level), 0);
    public bool GetUseHints() => _store.GetOrDefault(_useHints, true);
    public void SetUseHints(bool active) => _store.Put(_useHints, active);
    public int GetHintPoints() => _store.GetOrDefault(_hintPoints, 0);
    public void SetHintPoints(int hintPoints) => _store.Put(_hintPoints, hintPoints);
    public bool HasDailyBonusBeenGiven() => _store.GetOrDefault(_dailyBonusDate, -1) == DateTime.Today.DayOfYear;
    public void GiveDailyBonus() => _store.Put(_dailyBonusDate, DateTime.Today.DayOfYear);
    public int MinutesTilNextHintBonus() => _store.GetOrDefault(_minutesTilHintBonus, 0);
    public void SetMinutesTilNextHintBonus(int minutes) => _store.Put(_minutesTilHintBonus, minutes);
}
