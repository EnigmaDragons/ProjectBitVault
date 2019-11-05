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
    [SerializeField] private List<GameObject> selectableObjects = new List<GameObject>();
    
    [SerializeField] private List<MovementRule> movementRules = new List<MovementRule>();

    public TilePoint BitVaultLocation => bitVaultLocation;
    public int NumSelectableObjects => selectableObjects.Count;
    public List<MovementRule> MovementRules => movementRules;

    public void InitLevel()
    {
        bitVaultLocation = null;
        walkableTiles = new List<TilePoint>();
        blockedTiles = new List<TilePoint>();
        jumpableObjects = new List<TilePoint>();
        selectableObjects = new List<GameObject>();
        movementRules = new List<MovementRule>();
    }

    public void AddMovementRule(MovementRule rule) => movementRules.Add(rule);
    
    public void RegisterAsSelectable(GameObject obj) => selectableObjects.Add(obj);
    public void RegisterAsJumpable(GameObject obj) => jumpableObjects.Add(new TilePoint(obj));
    public void RegisterBitVault(GameObject obj) => bitVaultLocation = new TilePoint(obj);
    public void RegisterWalkableTile(GameObject obj) => walkableTiles.Add(new TilePoint(obj));
    public void RegisterBlockingObject(GameObject obj) => blockedTiles.Add(new TilePoint(obj));
    
    public void Remove(GameObject obj)
    {
        var tile = new TilePoint(obj);
        jumpableObjects.RemoveAll(o => o.Equals(tile));
        blockedTiles.RemoveAll(o => o.Equals(tile));
        selectableObjects.Remove(obj);
    }

    public Maybe<GameObject> GetSelectable(TilePoint tile) => selectableObjects.FirstAsMaybe(o => new TilePoint(o).Equals(tile));
    public bool IsJumpable(Vector3 position) => IsJumpable(new TilePoint(position));
    public bool IsJumpable(TilePoint tile) => jumpableObjects.Any(t => t.Equals(tile));
    public bool IsWalkable(Vector3 position) => IsWalkable(new TilePoint(position)) && !IsBlocked(new TilePoint(position)); // Perf
    public bool IsWalkable(TilePoint tile) => walkableTiles.Any(t => t.Equals(tile));
    public bool IsBlocked(TilePoint tile) => blockedTiles.Any(t => t.Equals(tile));

    public void Move(GameObject obj, TilePoint from, TilePoint to)
    {
        jumpableObjects.RemoveAll(o => o.Equals(from));
        blockedTiles.RemoveAll(o => o.Equals(from));
        blockedTiles.Add(to);
        jumpableObjects.Add(to);
    }
}

