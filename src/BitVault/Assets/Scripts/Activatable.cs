using UnityEngine;

public interface  Activatable
{
    bool CanActivate(GameObject piece);
    GameObject GameObject { get; }
    void Activate();
}

public static class ActivatableExtensions
{
    public static TilePoint Tile(this Activatable a) => new TilePoint(a.GameObject);
    public static void ActivateIfAllowed(this Activatable a, GameObject piece)
    {
        if (a.CanActivate(piece))
            a.Activate();
    }
}
