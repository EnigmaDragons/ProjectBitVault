using System.Collections;
using UnityEngine;

public class NavigateToLevelSelectOnLevelComplete : OnMessage<LevelCompleted>
{
    [SerializeField] private Navigator navigator;

    protected override void Execute(LevelCompleted msg) => StartCoroutine(NavigateAfterDelay());

    private IEnumerator NavigateAfterDelay()
    {
        yield return new WaitForSeconds(1);
        navigator.NavigateToLevelSelect();
    }
}
  
