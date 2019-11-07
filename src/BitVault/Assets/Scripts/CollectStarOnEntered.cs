public sealed class CollectStarOnEntered : OnMessage<PieceMoved>
{
    protected override void Execute(PieceMoved msg)
    {
        if (!new TilePoint(gameObject).Equals(msg.To)) return;
        
        Message.Publish(new StarCollected());
        Destroy(gameObject);
    }
}
