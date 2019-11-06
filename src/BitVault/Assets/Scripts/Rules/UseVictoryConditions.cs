using System.Linq;
using UnityEngine;

public class UseVictoryConditions : MonoBehaviour
{
    [SerializeField] private CurrentLevelMap map;
    [SerializeField] private VictoryCondition[] conditions;
    [SerializeField] private GameEvent win;
    
    private void OnEnable() => Message.Subscribe<PieceMoved>(_ => CheckForVictory(), this);
    private void OnDisable() => Message.Unsubscribe(this);

    private void CheckForVictory()
    {
        if (conditions.All(x => x.HasCompletedLevel(map)))
            win.Publish();
    }
}
