using UnityEngine;

public class GameState : ScriptableObject
{
    [SerializeField] private CurrentLevelMap currentLevelMap;
    [SerializeField] private GameObject selectedLevel;
    
    public GameObject SelectedLevel => selectedLevel;

    public void SelectLevel(GameObject level) => selectedLevel = level;
}
