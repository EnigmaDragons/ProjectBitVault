using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class CurrentLevelMap : ScriptableObject
{
    [SerializeField] private List<TilePoint> walkableTiles = new List<TilePoint>();
    [SerializeField] private List<TilePoint> blockedTiles = new List<TilePoint>();
    [SerializeField] private List<GameObject> jumpableObjects = new List<GameObject>();
    [SerializeField] private TilePoint bitVaultLocation;

    public TilePoint BitVaultLocation => bitVaultLocation;

    public void InitLevel()
    {
        walkableTiles = new List<TilePoint>();
    }

    public void RegisterAsJumpable(GameObject obj) => jumpableObjects.Add(obj);
    public void RegisterBitVault(GameObject obj) => bitVaultLocation = new TilePoint(obj);
    public void RegisterWalkableTile(GameObject obj) => walkableTiles.Add(new TilePoint(obj));
    public void RegisterBlockingObject(GameObject obj) => blockedTiles.Add(new TilePoint(obj));

    public bool IsJumpable(Vector3 position) => IsJumpable(new TilePoint(position));
    public bool IsJumpable(TilePoint tile) => jumpableObjects.Any(o => new TilePoint(o).Equals(tile));
    public bool IsWalkable(Vector3 position) => IsWalkable(new TilePoint(position)) && !IsBlocked(new TilePoint(position));
    public bool IsWalkable(TilePoint tile) => walkableTiles.Any(t => t.Equals(tile));
    public bool IsBlocked(TilePoint tile) => blockedTiles.Any(t => t.Equals(tile));

}

