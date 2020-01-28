
public class BasicPuzzleComplexityStats
{
    public string LevelName { get; }
    public int NumSpaces { get; }
    public int NumJumps { get; }
    public int PieceComplexity { get; }
    public int Score => NumSpaces + NumJumps * 2 + PieceComplexity * 2;

    public BasicPuzzleComplexityStats(string name, int numSpaces, int numJumps, int pieceComplexity)
    {
        LevelName = name;
        NumSpaces = numSpaces;
        NumJumps = numJumps;
        PieceComplexity = pieceComplexity;
    }
    
    public override string ToString() => $"{LevelName}, {Score}, - , {NumSpaces}, {NumJumps}, {PieceComplexity}";
}