using UnityEngine;

public sealed class MoveProcessor : MonoBehaviour
{
    [SerializeField] private CurrentSelectedPiece piece;
    [SerializeField] private CurrentLevelMap map;
    
    private void OnEnable()
    {
        Message.Subscribe<MoveByRequested>(ProcessMoveByRequest, this);
        Message.Subscribe<TileIndicated>(ProcessTileIndicated, this);
    }
    
    private void OnDisable()
    {
        Message.Unsubscribe(this);
    }

    private void ProcessTileIndicated(TileIndicated t)
    {
        piece.Selected.IfPresent(p => Message.Publish(new MoveByRequested(t.Tile - new TilePoint(p.gameObject))));
    }
    
    private void ProcessMoveByRequest(MoveByRequested m)
    {
        piece.Selected.IfPresent(activePiece =>
        {
            if (MoveIsInvalid(m)) return;
            
            var pos = activePiece.transform.position;
            
            var oneSquareDelta = m.Delta / m.Delta.TotalMagnitude();
            var destination = oneSquareDelta.Plus(pos);
            var twoSquaresAway = oneSquareDelta.Plus(oneSquareDelta).Plus(pos);

            if (map.IsWalkable(destination))
                activePiece.transform.position = destination;
            else if (map.IsJumpable(destination) && map.IsWalkable(twoSquaresAway))
            {
                activePiece.transform.position = twoSquaresAway;
                Message.Publish(new TileJumped(new TilePoint(destination)));
            }
        });
    }

    private bool MoveIsInvalid(MoveByRequested m)
    {
        var t = m.Delta;
        if (!t.IsCardinal())
            return true;
        if (t.TotalMagnitude() > 2)
            return true;
        return false;
    }
}
