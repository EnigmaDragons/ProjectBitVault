using UnityEngine;

public class GainStarIfAllIceCleared : OnMessage<IceDestroyed>
{
    [SerializeField] private CurrentLevelMap map;

    protected override void Execute(IceDestroyed msg)
    {
        if (!map.IsIcePresent())
            Message.Publish(new StarCollected());
    }
}
