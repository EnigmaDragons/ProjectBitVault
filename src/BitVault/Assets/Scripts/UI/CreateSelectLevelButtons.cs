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

    private void Awake() => groups.Init(levelButton.gameObject, 
        zones.Value.Select((zone, zoneNum) => zone.Value
                .Select((level, levelNum) => InitLevelButton(zoneNum, levelNum, level))
                .ToList())
            .ToList());

    private Action<GameObject> InitLevelButton(int zone, int levelNum, GameLevel level)
    {
        Action<GameObject> init = button => button.GetComponent<LevelButton>().Init($"Level {levelNum + 1}", () =>
        {
            currentLevel.SelectLevel(zones.Value[zone].Value[levelNum], zone, levelNum);
            navigator.NavigateToGameScene();
        }, level);
        return init;
    }
}
  
