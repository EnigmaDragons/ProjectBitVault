
public sealed class DeselectPieceAfterEachJump : OnMessage<TileJumped>
{
    protected override void Execute(TileJumped msg) => Message.Publish(new PieceDeselected());
}
