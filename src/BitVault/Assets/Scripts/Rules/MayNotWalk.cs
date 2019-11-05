using UnityEngine;

public sealed class MayNotWalk : MovementRule
{
    public override bool IsValid(GameObject obj, MoveByRequested m) => m.Delta.TotalMagnitude() != 1;
}
