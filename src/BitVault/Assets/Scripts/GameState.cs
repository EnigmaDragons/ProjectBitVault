using UnityEngine;

public class GameState : ScriptableObject
{
    [SerializeField] private CurrentLevelMap currentLevelMap;
    [SerializeField] private CurrentSelectedPiece currentPiece;
    [SerializeField] private GameObject selectedLevel;
    [SerializeField] private CurrentLevelStars currentLevelStars;
    [DTValidator.Optional, SerializeField] private GameObject activeLevel;
    
    public void SelectLevel(GameObject level) => selectedLevel = level;
    
    public void InitLevel()
    {
        Message.Publish(new PieceDeselected());
        currentLevelStars.Reset();
        currentLevelMap.InitLevel();
        currentPiece.Deselect();
        DestroyImmediate(activeLevel);
        activeLevel = Instantiate(selectedLevel);
        Message.Publish(new LevelReset());
    }
}
