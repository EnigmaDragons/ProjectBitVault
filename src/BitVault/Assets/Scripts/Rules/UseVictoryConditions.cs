using System.Linq;
using UnityEngine;

public class UseVictoryConditions : OnMessage<PieceMoved>
{
    [SerializeField] private CurrentLevelMap map;
    [SerializeField] private VictoryCondition[] conditions;
    
    protected override void Execute(PieceMoved msg)
    {
        if (conditions.All(x => x.HasCompletedLevel(map))) 
            Message.Publish(new LevelCompleted());
    }
}
