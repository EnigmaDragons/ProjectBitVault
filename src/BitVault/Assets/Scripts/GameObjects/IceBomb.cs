using UnityEngine;

public class IceBomb : OnMessage<PieceMoved>
{
    protected override void Execute(PieceMoved msg)
    {
        if (msg.To.DistanceFrom(new TilePoint(gameObject)) == 1)
        {
            Message.Publish(new ObjectDestroyed(gameObject));
            Message.Publish(new ObjectDestroyed(msg.Piece));
        }
    }
}
