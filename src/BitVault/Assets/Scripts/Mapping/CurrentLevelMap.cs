using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class CurrentLevelMap : ScriptableObject
{
    [SerializeField] private TilePoint bitVaultLocation;
    [SerializeField] private List<TilePoint> walkableTiles = new List<TilePoint>();
    [SerializeField] private List<TilePoint> blockedTiles = new List<TilePoint>();
    [SerializeField] private List<TilePoint> jumpableObjects = new List<TilePoint>();

    public TilePoint BitVaultLocation => bitVaultLocation;

    public void InitLevel()
    {
        bitVaultLocation = null;
        walkableTiles = new List<TilePoint>();
        blockedTiles = new List<TilePoint>();
        jumpableObjects = new List<TilePoint>();
    }

    public void RegisterAsJumpable(GameObject obj) => jumpableObjects.Add(new TilePoint(obj));
    public void RegisterBitVault(GameObject obj) => bitVaultLocation = new TilePoint(obj);
    public void RegisterWalkableTile(GameObject obj) => walkableTiles.Add(new TilePoint(obj));
    public void RegisterBlockingObject(GameObject obj) => blockedTiles.Add(new TilePoint(obj));
    
    public void RemoveJumpable(GameObject obj) => jumpableObjects.RemoveAll(o => o.Equals(new TilePoint(obj))); // Perf
    public void RemoveBlocking(GameObject obj) => blockedTiles.RemoveAll(o => o.Equals(new TilePoint(obj))); // Perf

    public bool IsJumpable(Vector3 position) => IsJumpable(new TilePoint(position));
    public bool IsJumpable(TilePoint tile) => jumpableObjects.Any(t => t.Equals(tile));
    public bool IsWalkable(Vector3 position) => IsWalkable(new TilePoint(position)) && !IsBlocked(new TilePoint(position)); // Perf
    public bool IsWalkable(TilePoint tile) => walkableTiles.Any(t => t.Equals(tile));
    public bool IsBlocked(TilePoint tile) => blockedTiles.Any(t => t.Equals(tile));
}

