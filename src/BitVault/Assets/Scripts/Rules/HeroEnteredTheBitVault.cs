
public sealed class HeroEnteredTheBitVault : VictoryCondition
{
    public override bool HasCompletedLevel(CurrentLevelMap map) 
        => map.Hero.IsPresentAnd(h => 
            new TilePoint(h).Equals(map.BitVaultLocation.OrDefault(() => new TilePoint(int.MaxValue, int.MaxValue))));
}
