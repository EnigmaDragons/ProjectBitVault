public sealed class MoveToRequested
{
    public TilePoint Destination { get; }

    public MoveToRequested(TilePoint t) => Destination = t;
}
