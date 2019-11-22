using UnityEngine;

public class TileIndicatedProcessor : OnMessage<TileIndicated>
{
    [SerializeField] private CurrentSelectedPiece piece;
    [SerializeField] private CurrentLevelMap map;

    protected override void Execute(TileIndicated msg)
    {
        var selectable = map.GetSelectable(msg.Tile);
        if (selectable.IsPresent)
        {
            Message.Publish(new PieceSelected(selectable.Value));
            return;
        }
        
        piece.Selected.IfPresent(p => Message.Publish(new MoveToRequested(p, new TilePoint(p.gameObject), msg.Tile)));
    }
}
