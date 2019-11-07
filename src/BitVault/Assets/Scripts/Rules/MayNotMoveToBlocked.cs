using UnityEngine;

[CreateAssetMenu]
public sealed class MayNotMoveToBlocked : MovementRestrictionRule
{
    [SerializeField] private CurrentLevelMap map;

    public override bool IsValid(MovementProposed m) => !map.IsBlocked(m.To);
}
