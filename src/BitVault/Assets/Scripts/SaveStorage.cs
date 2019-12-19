using UnityEngine;
using System.Linq;

[CreateAssetMenu]
public class SaveStorage : ScriptableObject
{
    private const string _totalStarsKey = "TotalStars";
    private const string _zoneKey = "ZoneKey";
    private const string _versionKey = "Version";
    private const string _version = "0.1";
    private const string _showMovementHints = "ShowMovementHints";
    private const string _autoSkipStory = "AutoSkipStory"; 
    
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
}
