using System;

public class FallingTile : OnMessage<PieceMoved>
{
    protected override void Execute(PieceMoved msg)
    {
        if (msg.From.Equals(new TilePoint(gameObject)))
            Message.Publish(new ObjectDestroyed(gameObject));
    }
}
