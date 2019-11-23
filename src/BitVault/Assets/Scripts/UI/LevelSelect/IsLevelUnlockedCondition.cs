using UnityEngine;

[CreateAssetMenu]
public sealed class IsLevelUnlockedCondition : ScriptableObject
{
    [SerializeField] private GameZones zones;
    [SerializeField] private SaveStorage storage;
    [SerializeField] private BoolVariable isDevelopmentMode;
    
    public bool IsLevelUnlocked(int zoneNumber, int levelNumber)
    {
        if ((zoneNumber == 0 && levelNumber == 0) || isDevelopmentMode.Value)
            return true;
        var previousZone = levelNumber == 0 ? zoneNumber - 1 : zoneNumber;
        var previousLevel = levelNumber == 0 ? zones.Value[previousZone].Value.Length - 1 : levelNumber - 1;
        return storage.GetStars(zones.Value[previousZone].Value[previousLevel]) > 0 && storage.GetTotalStars() >= zones.Value[zoneNumber].StarsRequired;
    }
}
