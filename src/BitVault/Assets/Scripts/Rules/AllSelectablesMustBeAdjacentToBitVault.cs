using System.Linq;

public sealed class AllSelectablesMustBeAdjacentToBitVault : VictoryCondition
{
    public override bool HasCompletedLevel(CurrentLevelMap map) 
        => map.BitVaultLocation.IsPresentAnd(
            vault => map.Selectables.All(s => new TilePoint(s).IsAdjacentTo(vault)));
}
