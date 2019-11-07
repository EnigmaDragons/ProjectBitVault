    using UnityEngine;

    [CreateAssetMenu]
    public class CantMoveToBlocked : MovementRule
    {
        [SerializeField] private CurrentLevelMap map;

        public override bool IsValid(GameObject piece, MoveToRequested m) => !map.IsBlocked(m.To);
    }
