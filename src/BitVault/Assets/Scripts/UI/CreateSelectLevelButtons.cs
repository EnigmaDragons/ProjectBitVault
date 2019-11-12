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

    private void Awake() => groups.Init(levelButton.gameObject, zones.Value.Select((zone, zoneNum) => zone.Value.Select((_, levelNum) => InitLevelButton(zoneNum, levelNum)).ToList()).ToList());

    private Action<GameObject> InitLevelButton(int zone, int level)
    {
        Action<GameObject> init = button => button.GetComponent<LevelButton>().Init($"Level {level + 1}", () =>
        {
            currentLevel.SelectLevel(zones.Value[zone].Value[level], zone, level);
            navigator.NavigateToGameScene();
        });
        return init;
    }
}
  
