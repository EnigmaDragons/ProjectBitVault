using UnityEngine;

public sealed class MoveProcessor : MonoBehaviour
{
    [SerializeField] private CurrentLevelMap map;
    [ReadOnly, SerializeField] private GameObject activePiece;
    
    private void OnEnable()
    {
        Message.Subscribe<PieceSelected>(e => activePiece = e.Piece, this);
        Message.Subscribe<MoveToRequested>(ProcessMoveToRequest, this);
        Message.Subscribe<MoveByRequested>(ProcessMoveByRequest, this);
    }
    
    private void OnDisable()
    {
        Message.Unsubscribe(this);
    }

    private void ProcessMoveByRequest(MoveByRequested m)
    {
        if (activePiece == null) return;

        var pos = activePiece.transform.position;
        var destination = m.Delta.Plus(pos);
        var twoSquaresAway = m.Delta.Plus(m.Delta).Plus(pos);
        
        if (map.IsWalkable(destination))
            activePiece.transform.position = destination;
        else if (map.IsJumpable(destination) && map.IsWalkable(twoSquaresAway))
            activePiece.transform.position = twoSquaresAway;
    }

    private void ProcessMoveToRequest(MoveToRequested m)
    {
        if (activePiece == null) return;
        
        if (map.IsWalkable(m.Destination))
            activePiece.transform.position = new Vector3(m.Destination.X, m.Destination.Y, activePiece.transform.position.z);
    }
}
