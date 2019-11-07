using UnityEngine;

public sealed class MayOnlyMoveInCardinalDirections : MovementRule
{
    public override bool IsValid(GameObject obj, MoveToRequested m) => m.Delta.IsCardinal();
}
