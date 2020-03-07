using UnityEngine;

public class LineUpAchievement : OnMessage<PieceMoved, LevelCompleted>
{
    [SerializeField] private CurrentLevelMap map;
    [SerializeField] private Achievements achievements;
    [SerializeField] private BoolVariable isTutorial;

    private bool _failed = false;
    private int _moveCount = 0;

    protected override void Execute(PieceMoved msg)
    {
        if (msg.Piece == map.Hero)
            _moveCount++;
        else if (_moveCount > 0)
            _failed = true;
    }

    protected override void Execute(LevelCompleted msg)
    {
        if (_moveCount >= 5 && !_failed && !isTutorial.Value)
            achievements.UnlockAchievement(AchievementType.LineUp);
    }
}
