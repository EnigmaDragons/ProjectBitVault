using UnityEngine;

public class ActivationProcessor : OnMessage<TileIndicated>
{
    [SerializeField] private CurrentLevelMap map;
    [SerializeField] private CurrentSelectedPiece piece;
    
    protected override void Execute(TileIndicated msg)
    {
        piece.Selected.IfPresent(p => 
            map.GetActivatable(msg.Tile).IfPresent(a => 
                a.ActivateIfAllowed(p)));
    }
}
