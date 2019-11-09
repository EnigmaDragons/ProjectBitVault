using UnityEngine;

public class PlayerAttackAgainstIce : OnMessage<PieceMoved>
{
    [SerializeField] private CurrentLevelMap map;

    protected override void Execute(PieceMoved msg)
    {
        if (map.IsIce(msg.To))
        {
            map.DestroyIce(msg.To);
            Message.Publish(new ObjectDestroyed(gameObject));
        }
    }
}
