using System.Linq;
using UnityEngine;

public sealed class GainStarIfAllDataNodesRemoved : OnMessage<LevelStateChanged>
{
    [SerializeField] private CurrentLevelMap map;
    
    protected override void Execute(LevelStateChanged msg)
    {
        if (map.Selectables.Count() == 1 && map.Heroes.Count() == 1)
            Message.Publish(new StarCollected());
    }
}
