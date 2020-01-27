using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameLevelsDuplicatesTester : MonoBehaviour
{
    
#if UNITY_EDITOR
    void Awake()
    {
        var levels = new HashSet<string>();
        var gameLevels = UnityResourceUtils.FindAssetsByType<GameLevels>();
        gameLevels.SelectMany(zone => zone.Value).ForEach(level =>
        {
            var key = level.GetInstanceID().ToString();
            if (!levels.Add(key))
                Debug.LogError($"Duplicate of {level.Name} - {level.GetInstanceID()}");
        });
        Debug.Log($"Tested {levels.Count} Levels in {gameLevels.Count} Zones for Duplicates");
    }
#endif
    
}
