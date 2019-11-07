using UnityEngine;

public class DisappearIfJumped : OnMessage<PieceMoved>
{
    [SerializeField] private CurrentLevelMap map;

    protected override void Execute(PieceMoved msg)
    {
        if (msg.From.IsAdjacentTo(new TilePoint(gameObject)) && msg.To.IsAdjacentTo(new TilePoint(gameObject)) && (msg.To.X == msg.From.X || msg.To.Y == msg.From.Y))
        {
            map.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}
