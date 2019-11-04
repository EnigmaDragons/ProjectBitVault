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
            var pos = activePiece.transform.position;
            var destination = m.Delta.Plus(pos);
            var twoSquaresAway = m.Delta.Plus(m.Delta).Plus(pos);

            if (map.IsWalkable(destination))
                activePiece.transform.position = destination;
            else if (map.IsJumpable(destination) && map.IsWalkable(twoSquaresAway))
            {
                activePiece.transform.position = twoSquaresAway;
                Message.Publish(new TileJumped(new TilePoint(destination)));
            }
        });
    }
}
