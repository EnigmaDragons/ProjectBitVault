
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class CurrentLevelMap : ScriptableObject
{
    [SerializeField] private List<TilePoint> walkableTiles = new List<TilePoint>();

    public void InitLevel()
    {
        walkableTiles = new List<TilePoint>();
    }

    public void RegisterWalkableTile(GameObject obj) => walkableTiles.Add(new TilePoint(obj.transform.position));

    public bool IsWalkable(Vector3 position)
    {
        var tilepoint = new TilePoint(position);
        return walkableTiles.Any(t => t.Equals(tilepoint));
    }
}

