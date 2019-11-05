using UnityEngine;

public sealed class UpdateMapOnPieceMoved : OnMessage<PieceMoved>
{
    [SerializeField] private CurrentLevelMap map;
    
    protected override void Execute(PieceMoved msg) => map.Move(msg.Piece, msg.From, msg.To);
}
