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
        var movementProposals = map.MovementOptionRules.Where(x => x.IsPossible(m))
            .Select(x => new MovementProposed(x.Type, m.Piece, m.From, m.To));

        foreach (var proposal in movementProposals)
        {
            if (map.MovementRestrictionRules.All(x => x.IsValid(proposal)))
            {
                proposal.Piece.transform.position = new Vector3(m.To.X, m.To.Y, 0);
                Message.Publish(new PieceMoved(proposal.Piece, m.From, m.To));
                return;
            }
        }
    }
}
