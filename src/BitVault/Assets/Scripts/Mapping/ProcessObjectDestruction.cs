using UnityEngine;

public class ProcessObjectDestruction : OnMessage<ObjectDestroyed>
{
    [SerializeField] private CurrentLevelMap map;

    protected override void Execute(ObjectDestroyed msg)
    {
        map.Remove(msg.Object);
        if (!msg.IsGameObjectDestructionHandled)
            msg.Object.SetActive(false);
    }
}
