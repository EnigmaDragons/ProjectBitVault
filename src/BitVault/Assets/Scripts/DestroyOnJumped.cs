using UnityEngine;

public sealed class DestroyOnJumped : OnMessage<TileJumped>
{
    [SerializeField] private CurrentLevelMap map;
    
    protected override void Execute(TileJumped e)
    {
        if (!e.Tile.Equals(new TilePoint(gameObject))) return;
        
        map.RemoveJumpable(gameObject);
        map.RemoveBlocking(gameObject);
        Destroy(gameObject);
    }
}
