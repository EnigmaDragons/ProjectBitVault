using UnityEngine;

public sealed class PieceMoved
{
    public GameObject Piece { get; }
    public TilePoint From { get; }
    public TilePoint To { get; }

    public PieceMoved(GameObject obj, TilePoint from, TilePoint to)
    {
        Piece = obj;
        From = from;
        To = to;
    }
}
