using System.Linq;
using UnityEngine;

public sealed class MoveProcessor : MonoBehaviour
{
    [SerializeField] private CurrentSelectedPiece piece;
    [SerializeField] private CurrentLevelMap map;
    
    private void OnEnable()
    {
        Message.Subscribe<MoveToRequested>(ProcessMoveByRequest, this);
        Message.Subscribe<TileIndicated>(ProcessTileIndicated, this);
    }
    
    private void OnDisable()
    {
        Message.Unsubscribe(this);
    }

    private void ProcessTileIndicated(TileIndicated t)
    {
        piece.Selected.IfPresent(p => Message.Publish(new MoveToRequested(p, new TilePoint(p.gameObject), t.Tile)));
    }
    
    private void ProcessMoveByRequest(MoveToRequested m)
    {
        piece.Selected.IfPresent(activePiece =>
        {
            var pos = activePiece.transform.position;
            var destination = m.Delta.Plus(pos);

            // TODO: Convert IsWalkable Check to a rule
            if (map.IsWalkable(destination) && map.MovementRules.All(r => r.IsValid(activePiece, m)))
            {
                activePiece.transform.position = destination;
                Message.Publish(new PieceMoved(activePiece, new TilePoint(pos), new TilePoint(destination)));
            }
        });
    }
}
