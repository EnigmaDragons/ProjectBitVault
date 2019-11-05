using UnityEngine;

public sealed class IndicatedTileSelectionProcessor : OnMessage<TileIndicated>
{
    [SerializeField] private CurrentLevelMap map;
    [SerializeField] private CurrentSelectedPiece piece;

    protected override void Execute(TileIndicated msg)
    {
        if (piece.Selected.IsPresent) return;

        map.GetSelectable(msg.Tile)
            .IfPresent(o => Message.Publish(new PieceSelected(o)));
    }
}
