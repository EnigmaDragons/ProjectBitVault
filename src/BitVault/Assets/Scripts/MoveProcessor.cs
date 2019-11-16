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
        if (m.Piece.GetComponent<MovementEnabled>() == null)
            return;

        var movementProposals = map.MovementOptionRules
            .Where(r => m.Piece.GetComponent<MovementEnabled>().Types.Any(t => r.Type == t))
            .Where(x => x.IsPossible(m))
            .Select(x => new MovementProposed(x.Type, m.Piece, m.From, m.To)).ToList();

        foreach (var proposal in movementProposals)
        {
            if (map.MovementRestrictionRules.All(x => x.IsValid(proposal)))
            {
                Message.Publish(new PieceMoved(proposal.Piece, m.From, m.To));
                return;
            }
        }
    }
}
