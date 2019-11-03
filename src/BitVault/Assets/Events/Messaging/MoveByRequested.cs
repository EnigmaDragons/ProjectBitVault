public sealed class MoveByRequested
{
    public TilePoint Delta { get; }

    public MoveByRequested(TilePoint delta) => Delta = delta;
}
