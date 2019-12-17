using System;
using System.IO;
using System.Text;
using UnityEngine;
using File = UnityEngine.Windows.File;

[CreateAssetMenu]
public sealed class CurrentMapTokenizer : ScriptableObject
{
    private LevelMapBuilder _builder = new LevelMapBuilder();

    public void Init()
    {
        _builder = new LevelMapBuilder();
    }

    public void RegisterAsMapPiece(GameObject obj, MapPiece piece) => _builder = _builder.With(new TilePoint(obj), piece);

    public void ExportToFile()
    {
        var path = Path.Combine(Application.dataPath, $"{Guid.NewGuid().ToString()}");
        var levelString = new LevelMapAsString(_builder.Build()).ToString();
        File.WriteAllBytes(path, Encoding.UTF8.GetBytes(levelString));
        Debug.Log($"Wrote Level to {path}");
    }
}
