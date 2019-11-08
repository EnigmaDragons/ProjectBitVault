using UnityEngine;

public class MayAttack : MovementOptionRule
{
    [SerializeField] private CurrentLevelMap map;

    public override MovementType Type => MovementType.Attack;
    public override bool IsPossible(MoveToRequested m) => map.IsIce(m.To) && m.Delta.Distance() == 1;
}
