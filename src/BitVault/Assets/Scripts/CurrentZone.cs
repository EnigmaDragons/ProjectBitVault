using UnityEngine;

public class CurrentZone : ScriptableObject
{
    [SerializeField] private GameZones zones;
    [SerializeField] private GameLevels zone;

    public GameLevels Zone => zone;
    
    public void Init(int zoneIndex)
    {
        zone = zones.Value[zoneIndex];
    }
}
