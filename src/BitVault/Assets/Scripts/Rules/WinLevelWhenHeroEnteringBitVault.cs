using UnityEngine;

public class WinLevelWhenHeroEnteringBitVault : MonoBehaviour
{
    [SerializeField] private GameEvent onEntered;
    [SerializeField] private CurrentLevelMap levelMap;
    [SerializeField] private GameObject hero;

    private void Update()
    {
        if (levelMap.BitVaultLocation != null && new TilePoint(hero).Equals(levelMap.BitVaultLocation))
            onEntered.Publish();
    }
}
