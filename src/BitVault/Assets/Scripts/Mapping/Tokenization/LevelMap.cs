using System;

public sealed class LevelMap
{
    public string Name { get; }
    public MapPiece[,] FloorLayer { get; }
    public MapPiece[,] ObjectLayer { get; }
    
    public LevelMap(string name, MapPiece[,] floorLayer, MapPiece[,] objectLayer)
    {
        Name = name;
        if (floorLayer.Length != objectLayer.Length || floorLayer.Rank != objectLayer.Rank)
            throw new ArgumentException("FloorLayer and ObjectLayer are different sizes");
        FloorLayer = floorLayer;
        ObjectLayer = objectLayer;
    }
}
