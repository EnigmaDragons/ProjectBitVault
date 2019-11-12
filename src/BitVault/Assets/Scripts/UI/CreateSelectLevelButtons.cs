using System;
using System.Linq;
using UnityEngine;

public class CreateSelectLevelButtons : MonoBehaviour
{
    [SerializeField] private Navigator navigator;
    [SerializeField] private CurrentLevel currentLevel;
    [SerializeField] private GameLevels levels;
    [SerializeField] private LevelButton levelButton;
    [SerializeField] private MultiGridLayoutGroup groups;

    private void Awake() => groups.Init(levelButton.gameObject, levels.Value.Select((_, i) => InitLevelButton(i)).ToList());

    private Action<GameObject> InitLevelButton(int i)
    {
        Action<GameObject> init = button => button.GetComponent<LevelButton>().Init($"Level {i + 1}", () =>
        {
            currentLevel.SelectLevel(levels.Value[i], i);
            navigator.NavigateToGameScene();
        });
        return init;
    }
}
  
