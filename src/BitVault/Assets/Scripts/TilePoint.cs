using System;
using UnityEngine;

[Serializable]
public class TilePoint
{
    [SerializeField] public int X;
    [SerializeField] public int Y;

    public TilePoint() {}

    public TilePoint(GameObject o)
        : this(o.transform.position) {}
    
    public TilePoint(Vector3 v)
        : this(v.x.FlooredInt(), v.y.FlooredInt()) {}
    
    public TilePoint(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override int GetHashCode() => $"{X}{Y}".GetHashCode();
    public override bool Equals(object obj) => obj is TilePoint point && Equals(point);
    private bool Equals(TilePoint other) => other.X == X && other.Y == Y;
}
