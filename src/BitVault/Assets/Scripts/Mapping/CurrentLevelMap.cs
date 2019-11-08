using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CurrentLevelMap : ScriptableObject
{
    [DTValidator.Optional, SerializeField] private TilePoint bitVaultLocation;
    [SerializeField] private List<GameObject> heroes = new List<GameObject>();
    [SerializeField] private List<GameObject> ice = new List<GameObject>();
    [SerializeField] private List<GameObject> walkableTiles = new List<GameObject>();
    [SerializeField] private List<TilePoint> blockedTiles = new List<TilePoint>();
    [SerializeField] private List<TilePoint> jumpableObjects = new List<TilePoint>();
    [SerializeField] private List<Activatable> activatables = new List<Activatable>();
    [DTValidator.Optional, SerializeField] private List<GameObject> selectableObjects = new List<GameObject>();

    [SerializeField] private List<MovementOptionRule> movementOptionRules = new List<MovementOptionRule>();
    [SerializeField] private List<MovementRestrictionRule> movementRestrictionRules = new List<MovementRestrictionRule>();

    public List<GameObject> Heroes => heroes;
    public Maybe<TilePoint> BitVaultLocation => bitVaultLocation;
    public int NumSelectableObjects => selectableObjects.Count;
    public IEnumerable<MovementOptionRule> MovementOptionRules => movementOptionRules;
    public IEnumerable<MovementRestrictionRule> MovementRestrictionRules => movementRestrictionRules;
    public IEnumerable<GameObject> Selectables => selectableObjects;

    public void InitLevel()
    {
        bitVaultLocation = null;
        heroes = new List<GameObject>();
        ice = new List<GameObject>();
        walkableTiles = new List<GameObject>();
        blockedTiles = new List<TilePoint>();
        jumpableObjects = new List<TilePoint>();
        selectableObjects = new List<GameObject>();
        activatables = new List<Activatable>();
        movementOptionRules = new List<MovementOptionRule>();
        movementRestrictionRules = new List<MovementRestrictionRule>();
    }

    public void RegisterActivatable(Activatable a) => activatables.Add(a);
    public void RegisterHero(GameObject obj) => heroes.Add(obj);
    public void AddMovementOptionRule(MovementOptionRule optionRule) => movementOptionRules.Add(optionRule);
    public void AddMovementRestrictionRule(MovementRestrictionRule restrictionRule) => movementRestrictionRules.Add(restrictionRule);

    public void RegisterAsSelectable(GameObject obj) => selectableObjects.Add(obj);
    public void RegisterAsJumpable(GameObject obj) => jumpableObjects.Add(new TilePoint(obj));
    public void RegisterBitVault(GameObject obj) => bitVaultLocation = new TilePoint(obj);
    public void RegisterWalkableTile(GameObject obj) => walkableTiles.Add(obj);
    public void RegisterBlockingObject(GameObject obj) => blockedTiles.Add(new TilePoint(obj));
    public void RegisterIce(GameObject obj) => ice.Add(obj);

    public void Remove(GameObject obj)
    {
        var tile = new TilePoint(obj);
        walkableTiles.Remove(obj);
        jumpableObjects.RemoveAll(o => o.Equals(tile));
        blockedTiles.RemoveAll(o => o.Equals(tile));
        selectableObjects.Remove(obj);
        ice.Remove(obj);
        activatables.RemoveAll(a => a.GameObject.Equals(obj));
    }

    public Maybe<GameObject> GetTile(TilePoint tile) => walkableTiles.FirstAsMaybe(o => new TilePoint(o).Equals(tile));
    public Maybe<GameObject> GetSelectable(TilePoint tile) => selectableObjects.FirstAsMaybe(o => new TilePoint(o).Equals(tile));
    public Maybe<Activatable> GetActivatable(TilePoint tile) => activatables.FirstAsMaybe(a => a.Tile().Equals(tile));
    public bool IsJumpable(Vector3 position) => IsJumpable(new TilePoint(position));
    public bool IsJumpable(TilePoint tile) => jumpableObjects.Any(t => t.Equals(tile));
    public bool IsWalkable(Vector3 position) => IsWalkable(new TilePoint(position)) && !IsBlocked(new TilePoint(position)); // Perf
    public bool IsWalkable(TilePoint tile) => walkableTiles.Any(w => new TilePoint(w).Equals(tile));
    public bool IsBlocked(TilePoint tile) => blockedTiles.Any(t => t.Equals(tile));
    public bool IsIcePresent() => ice.Count > 0;
    public bool IsIce(TilePoint tile) => ice.Any(i => new TilePoint(i).Equals(tile));

    public void Move(GameObject obj, TilePoint from, TilePoint to)
    {
        jumpableObjects.RemoveAll(o => o.Equals(from));
        blockedTiles.RemoveAll(o => o.Equals(from));
        blockedTiles.Add(to);
        jumpableObjects.Add(to);
    }
}

