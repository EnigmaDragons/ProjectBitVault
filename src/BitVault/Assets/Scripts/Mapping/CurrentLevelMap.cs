using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class CurrentLevelMap : ScriptableObject
{
    [DTValidator.Optional, SerializeField] private TilePoint bitVaultLocation;
    [DTValidator.Optional, SerializeField] private GameObject hero;
    [DTValidator.Optional, SerializeField] private List<GameObject> walkableTiles = new List<GameObject>();
    [DTValidator.Optional, SerializeField] private List<GameObject> blockedTiles = new List<GameObject>();
    [DTValidator.Optional, SerializeField] private List<GameObject> jumpableObjects = new List<GameObject>();
    [DTValidator.Optional, SerializeField] private List<GameObject> selectableObjects = new List<GameObject>();
    [DTValidator.Optional, SerializeField] private List<GameObject> collectibleObjects = new List<GameObject>();
    [DTValidator.Optional, SerializeField] private Dictionary<GameObject, ObjectRules> destroyedObjects = new Dictionary<GameObject, ObjectRules>();

    [SerializeField] private List<MovementOptionRule> movementOptionRules = new List<MovementOptionRule>();
    [SerializeField] private List<MovementRestrictionRule> movementRestrictionRules = new List<MovementRestrictionRule>();

    public GameObject Hero => hero;
    public TilePoint BitVaultLocation => bitVaultLocation;
    public int NumSelectableObjects => selectableObjects.Count;
    public IEnumerable<MovementOptionRule> MovementOptionRules => movementOptionRules;
    public IEnumerable<MovementRestrictionRule> MovementRestrictionRules => movementRestrictionRules;
    public IEnumerable<GameObject> Selectables => selectableObjects;
    public int NumOfJumpables => jumpableObjects.Count;

    public void InitLevel()
    {
        bitVaultLocation = null;
        hero = null;
        walkableTiles = new List<GameObject>();
        blockedTiles = new List<GameObject>();
        jumpableObjects = new List<GameObject>();
        selectableObjects = new List<GameObject>();
        destroyedObjects = new Dictionary<GameObject, ObjectRules>();
        movementOptionRules = new List<MovementOptionRule>();
        movementRestrictionRules = new List<MovementRestrictionRule>();
    }

    public void RegisterHero(GameObject obj) => hero = obj;
    public void AddMovementOptionRule(MovementOptionRule optionRule) => movementOptionRules.Add(optionRule);
    public void AddMovementRestrictionRule(MovementRestrictionRule restrictionRule) => movementRestrictionRules.Add(restrictionRule);

    public void RegisterAsSelectable(GameObject obj) => selectableObjects.Add(obj);
    public void RegisterAsJumpable(GameObject obj) => jumpableObjects.Add(obj);
    public void RegisterBitVault(GameObject obj) => bitVaultLocation = new TilePoint(obj);
    public void RegisterWalkableTile(GameObject obj) => walkableTiles.Add(obj);
    public void RegisterBlockingObject(GameObject obj) => blockedTiles.Add(obj);
    public void RegisterAsCollectible(GameObject obj) => collectibleObjects.Add(obj);

    public Maybe<GameObject> GetTile(TilePoint tile) => walkableTiles.FirstAsMaybe(o => new TilePoint(o).Equals(tile));
    public Maybe<GameObject> GetSelectable(TilePoint tile) =>  selectableObjects.FirstAsMaybe(o => new TilePoint(o).Equals(tile));

    public bool IsJumpable(TilePoint tile) => jumpableObjects.Any(t => new TilePoint(t).Equals(tile));
    public bool IsWalkable(TilePoint tile) => walkableTiles.Any(w => new TilePoint(w).Equals(tile));
    public bool IsBlocked(TilePoint tile) => blockedTiles.Any(t => new TilePoint(t).Equals(tile));
    
    public void Move(GameObject obj, TilePoint from, TilePoint to)
        => Notify(() => {});

    public void RestoreDestroyed(GameObject obj)
    {
        Notify(() =>
        {
            if (!destroyedObjects.TryGetValue(obj, out var rules)) return;
            
            if (rules.IsJumpable)
                RegisterAsJumpable(obj);
            if (rules.IsBlocking)
                RegisterBlockingObject(obj);
            if (rules.IsSelectable)
                RegisterAsSelectable(obj);
            if (rules.IsWalkable)
                RegisterWalkableTile(obj);
            if (rules.IsCollectible)
                RegisterAsCollectible(obj);
        });
    }
    
    public void Remove(GameObject obj)
    {
        Notify(() =>
        {
            destroyedObjects[obj] = new ObjectRules
            {
                IsWalkable = walkableTiles.Remove(obj),
                IsJumpable = jumpableObjects.Remove(obj),
                IsBlocking = blockedTiles.Remove(obj),
                IsSelectable = selectableObjects.Remove(obj),
                IsCollectible = collectibleObjects.Remove(obj)
            };
        });
    }

    //This is code is a bit concrete
    public LevelSimulationSnapshot GetSnapshot()
    {
        var floors = new List<TilePoint>();
        var disengagedFailsafes = new List<TilePoint>();
        walkableTiles.ForEach(x =>
        {
            var fallingTile = x.GetComponent<FallingTile>();
            if (fallingTile && !fallingTile.IsDangerous)
                disengagedFailsafes.Add(new TilePoint(x));
            else if (!fallingTile)
                floors.Add(new TilePoint(x));
        });
        var oneHealthSubroutines = new List<TilePoint>();
        var twoHealthSubroutines = new List<TilePoint>();
        var iceSubroutines = new List<TilePoint>();
        jumpableObjects.ForEach(x =>
        {
            var doubleHealth = x.GetComponent<DestroyIfDoubleJumped>();
            if (doubleHealth && doubleHealth.NumJumpsRemaining == 2)
                twoHealthSubroutines.Add(new TilePoint(x));
            else if (doubleHealth && doubleHealth.NumJumpsRemaining == 1)
                oneHealthSubroutines.Add(new TilePoint(x));
            else if (x.GetComponent<TeleportingPiece>())
                iceSubroutines.Add(new TilePoint(x));
            else if (x.GetComponent<DestroyIfJumped>())
                oneHealthSubroutines.Add(new TilePoint(x));
        });
        return new LevelSimulationSnapshot(floors, disengagedFailsafes, oneHealthSubroutines, twoHealthSubroutines, iceSubroutines, collectibleObjects.Select(x => new TilePoint(x)).ToList(), new TilePoint(hero), bitVaultLocation);
    }
    
    private void Notify(Action a)
    {
        a();
        Message.Publish(new LevelStateChanged());
    }

    private class ObjectRules
    {
        public bool IsWalkable { get; set; }
        public bool IsJumpable { get; set; }
        public bool IsSelectable { get; set; }
        public bool IsBlocking { get; set; }
        public bool IsCollectible { get; set; }
    }
}

