using System.Linq;

public sealed class AllSelectablesMustBeAdjacentToBitVault : VictoryCondition
{
    public override bool HasCompletedLevel(CurrentLevelMap map)
        => map.Selectables.All(s => new TilePoint(s).IsAdjacentTo(map.BitVaultLocation));
}
