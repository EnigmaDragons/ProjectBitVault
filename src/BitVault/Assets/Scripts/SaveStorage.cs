using UnityEngine;

[CreateAssetMenu]
public class SaveStorage : ScriptableObject
{
    private const string _totalStarsKey = "TotalStars";
    private const string _zoneKey = "ZoneKey";
    private const string _versionKey = "Version";
    private const string _version = "0.1";
    private const string _showMovementHints = "ShowMovementHints";

    private void Awake() => PlayerPrefs.SetString(_versionKey, _version);

    public void SaveStars(GameLevel level, int stars)
    {
        var key = StarsKey(level);
        var previousStars = GetStars(level);
        if (previousStars < stars)
        {
            PlayerPrefs.SetInt(key, stars);
            AddToTotalStars(stars - previousStars);
        }
    }

    public int GetStars(GameLevel level)
    {
        var key = StarsKey(level);
        return PlayerPrefs.HasKey(key) ? PlayerPrefs.GetInt(key) : 0;
    }

    public int GetTotalStars() => PlayerPrefs.HasKey(_totalStarsKey) ? PlayerPrefs.GetInt(_totalStarsKey) : 0;
    
    public void SaveZone(int zone) => PlayerPrefs.SetInt(_zoneKey, zone);

    public int GetZone() => PlayerPrefs.HasKey(_zoneKey) ? PlayerPrefs.GetInt(_zoneKey) : 0;

    private void AddToTotalStars(int addedStars)
    {
        PlayerPrefs.SetInt(_totalStarsKey, addedStars + (PlayerPrefs.HasKey(_totalStarsKey) ? PlayerPrefs.GetInt(_totalStarsKey) : 0));
    }

    private string StarsKey(GameLevel level) => $"{level.Name}Stars";

    public bool GetShowMovementHints() => PlayerPrefs.HasKey(_showMovementHints) ? PlayerPrefs.GetInt(_showMovementHints) > 0 : true;
    public void SetShowMovementHints(bool active) => PlayerPrefs.SetInt(_showMovementHints, active ? 1 : 0);
}
