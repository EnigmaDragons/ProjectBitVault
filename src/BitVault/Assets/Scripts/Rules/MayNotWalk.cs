using UnityEngine;

public sealed class MayNotWalk : MovementRule
{
    public override bool IsValid(GameObject obj, MoveToRequested m) => m.Delta.TotalMagnitude() != 1;
}
