using UnityEngine;

public sealed class DestroyOnJumped : OnMessage<TileJumped>
{
    [SerializeField] private CurrentLevelMap map;
    
    protected override void Execute(TileJumped e)
    {
        if (!e.Tile.Equals(new TilePoint(gameObject))) return;
        
        map.Remove(gameObject);
        Destroy(gameObject);
    }
}
