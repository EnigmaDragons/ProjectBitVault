using UnityEngine;

public sealed class UseMovementRules : MonoBehaviour
{
    [SerializeField] private CurrentLevelMap map;
    [SerializeField] private MovementRule[] rules;

    private void Start() => rules.ForEach(r => map.AddMovementRule(r));
}
