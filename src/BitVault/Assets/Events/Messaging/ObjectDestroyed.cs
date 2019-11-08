using UnityEngine;

public class ObjectDestroyed
{
    public GameObject Object { get; }
    public TilePoint Location { get; }

    public ObjectDestroyed(GameObject o)
    {
        Object = o;
        Location = new TilePoint(o);
    }
}
