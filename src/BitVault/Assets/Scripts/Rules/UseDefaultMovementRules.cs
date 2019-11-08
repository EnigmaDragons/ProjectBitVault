using UnityEngine;

public sealed class UseDefaultMovementRules : OnMessage<LevelReset>
{
    [SerializeField] private CurrentLevelMap map;
    [SerializeField] private MovementOptionRule[] optionRules;
    [SerializeField] private MovementRestrictionRule[] restrictionRules;

    private void Start()
    {
        optionRules.ForEach(r => map.AddMovementOptionRule(r));
        restrictionRules.ForEach(r => map.AddMovementRestrictionRule(r));
    }

    protected override void Execute(LevelReset msg)
    {
        Start();
    }
}
