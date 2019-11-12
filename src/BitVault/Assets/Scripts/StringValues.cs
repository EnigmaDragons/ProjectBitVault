using System.Linq;
using UnityEngine;

public static class StringValues
{
    public static string LevelName(GameObject level) => level.name.Split('-').Last().WithSpaceBetweenWords();
    public static string StarsForLevel(GameObject level) => StarsForLevel(LevelName(level));
    public static string StarsForLevel(string levelName) => $"{levelName}Stars";
}
