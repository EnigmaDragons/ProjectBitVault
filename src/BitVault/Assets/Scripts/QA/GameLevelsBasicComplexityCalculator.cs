using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelsBasicComplexityCalculator : MonoBehaviour
{
    [SerializeField] private CurrentLevelMap map;
    [SerializeField] private CurrentLevel current;

    public IEnumerator AnalyzeAll()
    {
        var complexities = new List<string>();
        var gameLevels = UnityResourceUtils.FindAssetsByType<GameLevels>();
        foreach (var zone in gameLevels)
        {
            foreach (var level in zone.Value)
            {
                map.InitLevel(level.name);
                current.SelectLevel(level, -1, -1);
                current.Init();
                var levelMap = map.GetLevelMap();
                var stats = levelMap.BasicComplexityStats();
                complexities.Add(stats.ToString());
                Debug.Log(stats.ToString());
                yield return new WaitForEndOfFrame();
            }
        }

        Debug.Log($"Analyzed {complexities.Count} Levels in {gameLevels.Count} Zones for Complexity");
    }
    
    public void Analyze()
    {
        StartCoroutine(AnalyzeAll());
    }
}
