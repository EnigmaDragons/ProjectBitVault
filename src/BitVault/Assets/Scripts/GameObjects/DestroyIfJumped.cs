
public class DestroyIfJumped : OnMessage<PieceMoved>
{
    protected override void Execute(PieceMoved msg)
    {
        if (msg.From.IsAdjacentTo(new TilePoint(gameObject)) && msg.To.IsAdjacentTo(new TilePoint(gameObject)) && (msg.To.X == msg.From.X || msg.To.Y == msg.From.Y))
            Message.Publish(new ObjectDestroyed(gameObject));
    }
}
