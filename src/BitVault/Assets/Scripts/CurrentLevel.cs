using UnityEngine;

public class CurrentLevel : ScriptableObject
{
    [SerializeField] private GameLevel selectedLevel;
    [DTValidator.Optional, SerializeField] private GameObject activeLevelPrefab;
    [SerializeField] private int currentZoneNum;
    [SerializeField] private int currentLevelNum;

    public GameLevel ActiveLevel => selectedLevel;
    public int ZoneNumber => currentZoneNum;
    public int LevelNumber => currentLevelNum;

    public void SelectLevel(GameLevel level, int zoneNum, int levelNum)
    {
        selectedLevel = level;
        currentZoneNum = zoneNum;
        currentLevelNum = levelNum;
    }

    public void Init()
    {
        DestroyImmediate(activeLevelPrefab);
        activeLevelPrefab = Instantiate(selectedLevel.Prefab);
    } 
}
