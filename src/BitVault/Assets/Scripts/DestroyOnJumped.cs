
public sealed class DestroyOnJumped : OnMessage<TileJumped>
{
    protected override void Execute(TileJumped e)
    {
        if (e.Tile.Equals(new TilePoint(gameObject)))
            Destroy(gameObject);
    }
}
