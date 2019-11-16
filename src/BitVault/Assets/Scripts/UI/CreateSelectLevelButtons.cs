using System;
using System.Linq;
using UnityEngine;

public class CreateSelectLevelButtons : MonoBehaviour
{
    [SerializeField] private Navigator navigator;
    [SerializeField] private CurrentLevel currentLevel;
    [SerializeField] private GameZones zones;
    [SerializeField] private LevelButton levelButton;
    [SerializeField] private MultiGridLayoutGroup groups;
    [SerializeField] private SaveStorage storage;
    [SerializeField] private BoolVariable developmentToolsEnabled;

    private void Awake() => groups.Init(levelButton.gameObject, 
        zones.Value.Select((zone, zoneNum) => zone.Value
                .Select((level, levelNum) => InitLevelButton(zoneNum, levelNum, level))
                .ToList())
            .ToList(), storage.GetZone());

    private Action<GameObject> InitLevelButton(int zone, int levelNum, GameLevel level)
    {
        Action<GameObject> init = button => button.GetComponent<LevelButton>().Init($"{levelNum + 1}", () =>
        {
            currentLevel.SelectLevel(zones.Value[zone].Value[levelNum], zone, levelNum);
            navigator.NavigateToGameScene();
        }, level, IsLevelUnlocked(zone, levelNum));
        return init;
    }

    private bool IsLevelUnlocked(int zoneNumber, int levelNumber)
    {
        if ((zoneNumber == 0 && levelNumber == 0) || developmentToolsEnabled.Value)
            return true;
        var previousZone = levelNumber == 0 ? zoneNumber - 1 : zoneNumber;
        var previousLevel = levelNumber == 0 ? zones.Value[previousZone].Value.Length - 1 : levelNumber - 1;
        return storage.GetStars(zones.Value[previousZone].Value[previousLevel]) > 0 && storage.GetTotalStars() >= zones.Value[zoneNumber].StarsRequired;
    }
}
  
