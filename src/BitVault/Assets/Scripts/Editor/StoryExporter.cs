using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;

public class StoryExporter
{
    private const string StartLevel = "STARTING LEVEL";
    private const string DoingLevel = "LEVEL COMPLETED";

    [MenuItem("EnigmaDragons/Export Story")]
    public static void ExportStory()
    {
        ScriptableExtensions.GetAllInstances<GameZones>().ForEach(ExportStory);
    }

    private static void ExportStory(GameZones zones)
    {
        var path = EditorUtility.SaveFilePanel("Save Story To", "", "BitVaultStory.txt", "txt");
        if (path.Length == 0)
            return;
        File.WriteAllLines(path, zones.Value
            .SelectMany((zone, zoneI) => zone.Value
                .SelectMany((level, levelI) => new List<string> { $"SELECTED LEVEL {zoneI + 1}-{levelI + 1}: {level.Name}", "" }
                    .Concat(DialogueToStrings(level.OpeningDialogue))
                    .Concat(new List<string> { "", $"COMPLETED LEVEL {zoneI + 1}-{levelI + 1}: {level.Name}", "" })
                    .Concat(DialogueToStrings(level.ClosingDialogue))
                    .Concat(new List<string> { "" } ))));
    }

    private static List<string> DialogueToStrings(DialogueLine[] dialogue)
    {
        return dialogue.Select(x => $"{x.Character.Name}: {x.Text}").ToList();
    }
}
