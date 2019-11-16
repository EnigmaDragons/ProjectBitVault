using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CurrentLevelMap : ScriptableObject
{
    [DTValidator.Optional, SerializeField] private TilePoint bitVaultLocation;
    [DTValidator.Optional, SerializeField] private GameObject hero;
    [DTValidator.Optional, SerializeField] private List<GameObject> ice = new List<GameObject>();
    [DTValidator.Optional, SerializeField] private List<GameObject> walkableTiles = new List<GameObject>();
    [DTValidator.Optional, SerializeField] private List<GameObject> blockedTiles = new List<GameObject>();
    [DTValidator.Optional, SerializeField] private List<GameObject> jumpableObjects = new List<GameObject>();
    [DTValidator.Optional, SerializeField] private List<Activatable> activatables = new List<Activatable>();
    [DTValidator.Optional, SerializeField] private List<GameObject> selectableObjects = new List<GameObject>();

    [SerializeField] private List<MovementOptionRule> movementOptionRules = new List<MovementOptionRule>();
    [SerializeField] private List<MovementRestrictionRule> movementRestrictionRules = new List<MovementRestrictionRule>();

    public GameObject Hero => hero;
    public TilePoint BitVaultLocation => bitVaultLocation;
    public int NumSelectableObjects => selectableObjects.Count;
    public IEnumerable<MovementOptionRule> MovementOptionRules => movementOptionRules;
    public IEnumerable<MovementRestrictionRule> MovementRestrictionRules => movementRestrictionRules;
    public IEnumerable<GameObject> Selectables => selectableObjects;

    public void InitLevel()
    {
        bitVaultLocation = null;
        hero = null;
        ice = new List<GameObject>();
        walkableTiles = new List<GameObject>();
        blockedTiles = new List<GameObject>();
        jumpableObjects = new List<GameObject>();
        selectableObjects = new List<GameObject>();
        activatables = new List<Activatable>();
        movementOptionRules = new List<MovementOptionRule>();
        movementRestrictionRules = new List<MovementRestrictionRule>();
    }

    public void RegisterActivatable(Activatable a) => activatables.Add(a);
    public void RegisterHero(GameObject obj) => hero = obj;
    public void AddMovementOptionRule(MovementOptionRule optionRule) => movementOptionRules.Add(optionRule);
    public void AddMovementRestrictionRule(MovementRestrictionRule restrictionRule) => movementRestrictionRules.Add(restrictionRule);

    public void RegisterAsSelectable(GameObject obj) => selectableObjects.Add(obj);
    public void RegisterAsJumpable(GameObject obj) => jumpableObjects.Add(obj);
    public void RegisterBitVault(GameObject obj) => bitVaultLocation = new TilePoint(obj);
    public void RegisterWalkableTile(GameObject obj) => walkableTiles.Add(obj);
    public void RegisterBlockingObject(GameObject obj) => blockedTiles.Add(obj);
    public void RegisterIce(GameObject obj) => ice.Add(obj);

    public Maybe<GameObject> GetTile(TilePoint tile) => walkableTiles.FirstAsMaybe(o => new TilePoint(o).Equals(tile));
    public Maybe<GameObject> GetSelectable(TilePoint tile) => selectableObjects.FirstAsMaybe(o => new TilePoint(o).Equals(tile));
    public Maybe<Activatable> GetActivatable(TilePoint tile) => activatables.FirstAsMaybe(a => a.Tile().Equals(tile));
    public bool IsJumpable(TilePoint tile) => jumpableObjects.Any(t => new TilePoint(t).Equals(tile));
    public bool IsWalkable(TilePoint tile) => walkableTiles.Any(w => new TilePoint(w).Equals(tile));
    public bool IsBlocked(TilePoint tile) => blockedTiles.Any(t => new TilePoint(t).Equals(tile));
    public bool IsIcePresent() => ice.Count > 0;
    public bool IsIce(TilePoint tile) => ice.Any(i => new TilePoint(i).Equals(tile));
    
    public void DestroyIce(TilePoint tile) => Notify(() => ice.Where(x => tile.Equals(new TilePoint(x))).ForEach(x => Message.Publish(new ObjectDestroyed(x))));

    public void Move(GameObject obj, TilePoint from, TilePoint to)
        => Notify(() => {});
    
    public void Remove(GameObject obj)
    {
        Notify(() =>
        {
            walkableTiles.Remove(obj);
            jumpableObjects.Remove(obj);
            blockedTiles.Remove(obj);
            selectableObjects.Remove(obj);
            ice.Remove(obj);
            activatables.RemoveAll(a => a.GameObject.Equals(obj));
        });
    }
    
    private void Notify(Action a)
    {
        a();
        Message.Publish(new LevelStateChanged());
    }
}

