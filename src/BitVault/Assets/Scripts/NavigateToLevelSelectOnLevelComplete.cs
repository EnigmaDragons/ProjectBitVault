using UnityEngine;

public class NavigateToLevelSelectOnLevelComplete : OnMessage<LevelCompleted>
{
    [SerializeField] private Navigator navigator;

    protected override void Execute(LevelCompleted msg) => navigator.NavigateToLevelSelect();
}
  
