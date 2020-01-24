using UnityEngine;

[CreateAssetMenu]
public sealed class IsLevelUnlockedCondition : ScriptableObject
{
    [SerializeField] private Campaign zones;
    [SerializeField] private SaveStorage storage;
    [SerializeField] private BoolVariable isDevelopmentMode;
    
    public bool IsLevelUnlocked(int zoneNumber, int levelNumber)
    {
        if (levelNumber == 0 || isDevelopmentMode.Value)
            return true;
        var levelsCompleted = storage.GetLevelsCompletedInZone(zones.Value[zoneNumber]);
        return levelsCompleted > 0 && levelsCompleted > levelNumber - 3;
    }
}
