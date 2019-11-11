using System.Linq;

public sealed class HeroEnteredTheBitVault : VictoryCondition
{
    public override bool HasCompletedLevel(CurrentLevelMap map) 
        => map.Heroes.Any(h => 
            new TilePoint(h).Equals(map.BitVaultLocation));
}
