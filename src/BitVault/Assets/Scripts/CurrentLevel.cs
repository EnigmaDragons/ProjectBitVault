using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class CurrentLevel : ScriptableObject
{
    [SerializeField] private GameObject selectedLevel;
    [DTValidator.Optional, SerializeField] private GameObject activeLevel;
    [SerializeField] private int currentZoneNum;
    [SerializeField] private int currentLevelNum;

    public string ActiveLevelName => StringValues.LevelName(selectedLevel);
    public int ZoneNumber => currentZoneNum;
    public int LevelNumber => currentLevelNum;

    public void SelectLevel(GameObject level, int zoneNum, int levelNum)
    {
        selectedLevel = level;
        currentZoneNum = zoneNum;
        currentLevelNum = levelNum;
    }

    public void Init()
    {
        DestroyImmediate(activeLevel);
        activeLevel = Instantiate(selectedLevel);
    } 
}
