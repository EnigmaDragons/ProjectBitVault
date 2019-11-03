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

        var destination = m.Delta.Plus(activePiece.transform.position);
        if (map.IsWalkable(destination))
            activePiece.transform.position = new Vector3(destination.x, destination.y, activePiece.transform.position.z);
    }

    private void ProcessMoveToRequest(MoveToRequested m)
    {
        if (activePiece == null) return;
        
        if (map.IsWalkable(m.Destination))
            activePiece.transform.position = new Vector3(m.Destination.X, m.Destination.Y, activePiece.transform.position.z);
    }
}
