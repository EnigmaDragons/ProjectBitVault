using UnityEngine;

public sealed class MustJump : MovementRule
{
    public override bool IsValid(GameObject piece, MoveToRequested m) 
        => m.From.InBetween(m.To).Count == 1 && (m.Delta.X == 0 || m.Delta.Y == 0);
}
