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

    public bool IsCardinal() => (X == 0 && Y != 0) || (X != 0 && Y == 0);
    public int TotalMagnitude() => Math.Abs(X) + Math.Abs(Y);
    
    public override string ToString() => $"{X},{Y}";
    public override int GetHashCode() => ToString().GetHashCode();
    public override bool Equals(object obj) => obj is TilePoint point && Equals(point);
    private bool Equals(TilePoint other) => other.X == X && other.Y == Y;

    public Vector3 Plus(Vector3 v) => v + new Vector3(X, Y, 0);
    public TilePoint Plus(TilePoint t) => t + this;
    public static TilePoint operator +(TilePoint t, TilePoint t2) => new TilePoint(t.X + t2.X, t.Y + t2.Y);
    public static TilePoint operator -(TilePoint t, TilePoint t2) => new TilePoint(t.X - t2.X, t.Y - t2.Y);
    public static TilePoint operator /(TilePoint t, int divisor) => new TilePoint(t.X / divisor, t.Y / divisor);
}
