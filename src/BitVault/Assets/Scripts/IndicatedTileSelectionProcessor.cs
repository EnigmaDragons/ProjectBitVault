using UnityEngine;

public sealed class IndicatedTileSelectionProcessor : OnMessage<TileIndicated>
{
    [SerializeField] private CurrentLevelMap map;

    protected override void Execute(TileIndicated msg) 
        => map.GetSelectable(msg.Tile)
            .IfPresent(o => Message.Publish(new PieceSelected(o)));
}
