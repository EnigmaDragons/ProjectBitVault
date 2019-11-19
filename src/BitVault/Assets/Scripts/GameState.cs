using UnityEngine;

public class GameState : ScriptableObject
{
    [SerializeField] private CurrentLevelMap currentLevelMap;
    [SerializeField] private CurrentSelectedPiece currentPiece;
    [SerializeField] private CurrentLevelStars currentLevelStars;
    [SerializeField] private CurrentMoveCounter currentMoveCounter;
    [SerializeField] private CurrentLevel currentLevel;

    public void InitLevel()
    {
        currentLevelStars.Reset();
        currentMoveCounter.Reset();
        currentLevelMap.InitLevel();
        currentPiece.Deselect();
        currentLevel.Init();
        Message.Publish(new LevelReset());
    }
}
