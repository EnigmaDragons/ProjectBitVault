using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class CurrentLevel : ScriptableObject
{
    [SerializeField] private GameObject selectedLevel;
    [DTValidator.Optional, SerializeField] private GameObject activeLevel;
    [SerializeField] private int currentLevelNum;

    public string ActiveLevelName => selectedLevel.name.Split('-').Last().WithSpaceBetweenWords();
    public int NextLevel => currentLevelNum + 1;

    public void SelectLevel(GameObject level, int levelNum)
    {
        selectedLevel = level;
        currentLevelNum = levelNum;
    }

    public void Init()
    {
        DestroyImmediate(activeLevel);
        activeLevel = Instantiate(selectedLevel);
    } 
}
