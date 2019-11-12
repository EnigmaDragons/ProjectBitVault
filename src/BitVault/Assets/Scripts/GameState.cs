using UnityEngine;

public class GameState : ScriptableObject
{
    [SerializeField] private CurrentLevelMap currentLevelMap;
    [SerializeField] private CurrentSelectedPiece currentPiece;
    [SerializeField] private CurrentLevelStars currentLevelStars;
    [SerializeField] private CurrentMoveCounter currentMoveCounter;
    [SerializeField] private CurrentLevel currentLevel;
    [SerializeField] private BoolVariable gameInputActive;

    public void InitLevel()
    {
        Message.Publish(new PieceDeselected());
        currentLevelStars.Reset();
        currentMoveCounter.Reset();
        currentLevelMap.InitLevel();
        currentPiece.Deselect();
        currentLevel.Init();
        gameInputActive.Value = true;
        Message.Publish(new LevelReset());
    }
}
