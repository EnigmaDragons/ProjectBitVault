using System;

public sealed class LevelMap
{
    public MapPiece[,] FloorLayer { get; }
    public MapPiece[,] ObjectLayer { get; }
    
    public LevelMap(MapPiece[,] floorLayer, MapPiece[,] objectLayer)
    {
        if (floorLayer.Length != objectLayer.Length || floorLayer.Rank != objectLayer.Rank)
            throw new ArgumentException("FloorLayer and ObjectLayer are different sizes");
        FloorLayer = floorLayer;
        ObjectLayer = objectLayer;
    }
}
