using UnityEngine;

public class CurrentZone : ScriptableObject
{
    [SerializeField] private Campaign zones;
    [SerializeField] private GameLevels zone;
    [SerializeField] private GameEvent onCurrentZoneChanged;
    [SerializeField, ReadOnly] private int zoneIndex = 0;
    
    public GameEvent OnCurrentZoneChanged => onCurrentZoneChanged;
    public GameLevels Zone => zone;
    public int ZoneIndex => zoneIndex;
    
    public void Init(int zoneNumber)
    {
        zoneIndex = zoneNumber;
        zone = zones.Value[zoneIndex];
        onCurrentZoneChanged.Publish();
    }

    public void Init(Campaign campaign)
    {
        zones = campaign;
        Init(0);
    }
}
