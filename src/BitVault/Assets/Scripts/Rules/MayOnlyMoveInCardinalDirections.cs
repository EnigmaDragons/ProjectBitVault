using UnityEngine;

public sealed class MayOnlyMoveInCardinalDirections : MovementRule
{
    public override bool IsValid(GameObject obj, MoveByRequested m) => m.Delta.IsCardinal();
}
