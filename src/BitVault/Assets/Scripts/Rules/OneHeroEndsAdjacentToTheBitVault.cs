using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public sealed class OneHeroEndsAdjacentToTheBitVault : VictoryCondition
{
    public override bool HasCompletedLevel(CurrentLevelMap map)
        => map.Heroes.Any(h => new TilePoint(h).IsAdjacentTo(map.BitVaultLocation));
}
