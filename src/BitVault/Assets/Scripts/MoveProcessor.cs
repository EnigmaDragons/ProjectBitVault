using System.Linq;
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
            var twoSquareDelta = oneSquareDelta * 2;
            var destination = oneSquareDelta.Plus(pos);
            var twoSquaresAway = oneSquareDelta.Plus(oneSquareDelta).Plus(pos);

            // TODO: Convert IsWalkable Check to a rule
            if (map.IsWalkable(destination) && map.MovementRules.All(r => r.IsValid(activePiece, new MoveByRequested(oneSquareDelta))))
            {
                activePiece.transform.position = destination;
                Message.Publish(new PieceMoved(activePiece, new TilePoint(pos), new TilePoint(destination)));
            }
            else if (map.IsJumpable(destination) && map.IsWalkable(twoSquaresAway) && map.MovementRules.All(r => r.IsValid(activePiece, new MoveByRequested(twoSquareDelta))))
            {
                activePiece.transform.position = twoSquaresAway;
                Message.Publish(new PieceMoved(activePiece, new TilePoint(pos), new TilePoint(twoSquaresAway)));
                Message.Publish(new TileJumped(new TilePoint(destination)));
            }
        });
    }

    private bool MoveIsInvalid(MoveByRequested m)
    {
        // TODO: Convert IsCardinal to a Movement Rule
        var t = m.Delta;
        if (!t.IsCardinal())
            return true;
        // TODO: Convert MaxDistance to a Movement Rule
        if (t.TotalMagnitude() > 2)
            return true;
        return false;
    }
}
