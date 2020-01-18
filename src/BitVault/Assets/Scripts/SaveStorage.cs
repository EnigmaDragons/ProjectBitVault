using System;
using UnityEngine;
using System.Linq;
using JetBrains.Annotations;

[CreateAssetMenu]
public class SaveStorage : ScriptableObject
{
    private const string _totalStarsKey = "TotalStars";
    private const string _zoneKey = "ZoneKey";
    private const string _versionKey = "Version";
    private const string _version = "0.1";
    private const string _showMovementHints = "ShowMovementHints";
    private const string _autoSkipStory = "AutoSkipStory";
    private const string _useFemale = "UseFemale";
    private const string _useHints = "UseHints";
    private const string _hintPoints = "HintPoints";
    private const string _dailyBonusDate = "DailyBonusDate";
    private const string _minutesTilHintBonus = "MinutesTilHintBonus";
    
    private PlayerPrefsKeyValueStore _store = new PlayerPrefsKeyValueStore();
    
    public void Init()
    {
        if (_store == null)
            _store = new PlayerPrefsKeyValueStore();
        _store.Put(_versionKey, _version);
    }
    
    private string StarsKey(GameLevel level) => $"{level.Name}Stars";
    public int GetStars(GameLevel level) => _store.GetOrDefault(StarsKey(level), 0);
    public void SaveStars(GameLevel level, int stars)
    {
        var key = StarsKey(level);
        var previousStars = GetStars(level);
        if (previousStars < stars)
        {
            _store.Put(key, stars);
            AddToTotalStars(stars - previousStars);
        }
    }

    public int GetLevelsCompletedInZone(GameLevels zone) => zone.Value.Count(level => GetStars(level) > 0);

    public int GetZone() => _store.GetOrDefault(_zoneKey, 0);
    public void SaveZone(int zone) => _store.Put(_zoneKey, zone);

    public int GetTotalStars() => _store.GetOrDefault(_totalStarsKey, 0);
    private void AddToTotalStars(int addedStars) => _store.Update(_totalStarsKey, 0, existing => existing + addedStars);

    public bool GetShowMovementHints() => _store.GetOrDefault(_showMovementHints, true);
    public void SetShowMovementHints(bool active) => _store.Put(_showMovementHints, active);

    public bool GetAutoSkipStory() => _store.GetOrDefault(_autoSkipStory, false);
    public void SetAutoSkipStory(bool active) => _store.Put(_autoSkipStory, active);

    public bool HasChosenGender() => _store.Exists(_useFemale);
    public bool GetUseFemale() => _store.GetOrDefault(_useFemale, false);
    public void SetUseFemale(bool active) => _store.Put(_useFemale, active);

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

    public void Reset()
    {
        var showMovementHints = GetShowMovementHints();
        _store.Clear();
        SetShowMovementHints(showMovementHints);
    }
}
