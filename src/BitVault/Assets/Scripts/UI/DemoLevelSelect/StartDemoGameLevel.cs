using System.Collections;
using UnityEngine;

public sealed class StartDemoGameLevel : OnMessage<StartDemoLevelRequested, LevelCompleted, DemoQuitLevelRequested>
{
    [SerializeField] private GameObject game;

    private GameObject _current;
    
    protected override void Execute(StartDemoLevelRequested msg)
    {
        _current = Instantiate(game);
    }

    protected override void Execute(LevelCompleted msg) => StartCoroutine(ResolveLevelEnd(3f));
    protected override void Execute(DemoQuitLevelRequested msg) => StartCoroutine(ResolveLevelEnd(0f));

    private IEnumerator ResolveLevelEnd(float delay)
    {
        yield return new WaitForSeconds(delay);
        KillCurrent();
    }

    private void KillCurrent()
    {
        if (_current == null)
            return;
        
        var theGame = _current;
        _current = null;
        Destroy(theGame);
    }
}
