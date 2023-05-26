using UnityEngine;

public class ChangeMaterialIfNextToGoal : OnMessage<LevelReset>
{
    [SerializeField] private CurrentLevelMap map;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material swapTo;
    
    protected override void Execute(LevelReset msg)
    {        
        if (map.BitVaultLocation != null)
            if (map.BitVaultLocation.IsAdjacentTo(new TilePoint(gameObject)))
                meshRenderer.material = swapTo;
    }
}
