using UnityEngine;

[CreateAssetMenu]
public sealed class MayNotMoveToBlockedTiles : MovementRule
{
    [SerializeField] private CurrentLevelMap map;

    public override bool IsValid(GameObject obj, MoveByRequested m)
    {
        var destination = new TilePoint(obj) + m.Delta;
        return !map.IsBlocked(destination);
    }
}
