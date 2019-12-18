using System.Text;

public class LevelMapAsString
{
    private readonly LevelMap _map;
    private MapPiece[,] Floor => _map.FloorLayer;
    private MapPiece[,] Objects => _map.ObjectLayer;
        
    public LevelMapAsString(LevelMap map)
    {
        _map = map;
    }

    private string Separator => ">";
    private string Header => $"BitVaultMap";
    private string Size => $"Size[{Floor.Length},{Floor.Rank}";
    private string FloorLayer => LayerToString(Floor);
    private string ObjectLayer => LayerToString(Objects);

    private string LayerToString(MapPiece[,] layer)
    {
        var sb = new StringBuilder();
        for (var x = 0; x < layer.Length; x++)
            for (var y = 0; y < layer.Rank; y++)
                sb.Append(MapPieceSymbol.Symbol(layer[x, y]));
        return sb.ToString();
    }

    public override string ToString() => string.Join(Separator, Header, Size, FloorLayer, ObjectLayer);
}
