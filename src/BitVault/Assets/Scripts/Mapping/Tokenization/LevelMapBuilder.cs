using System;

public sealed class LevelMapBuilder
{
    private readonly MapPiece[,] _floors;
    private readonly MapPiece[,] _objects;
    
    public LevelMapBuilder() : this(7, 13) {}
    public LevelMapBuilder(int width, int height)
    {
        _floors = new MapPiece[width, height];
        _objects = new MapPiece[width, height];
    }

    public LevelMapBuilder With(TilePoint tile, MapPiece piece) => MapPieceSymbol.IsFloor(piece) ? WithFloor(tile, piece) : WithPiece(tile, piece);

    public LevelMapBuilder WithFloor(TilePoint tile, MapPiece piece)
    {
        if (!MapPieceSymbol.IsFloor(piece))
            throw new ArgumentException($"{piece} is not a floor piece.");
        _floors[tile.X, tile.Y] = piece;
        return this;
    }
    
    public LevelMapBuilder WithPiece(TilePoint tile, MapPiece piece)
    {
        if (!MapPieceSymbol.IsObject(piece))
            throw new ArgumentException($"{piece} is not an object piece.");
        _floors[tile.X, tile.Y] = piece;
        return this;
    }
    
    public LevelMap Build() => new LevelMap(_floors, _objects);
}

