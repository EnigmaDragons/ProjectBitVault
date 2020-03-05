using UnityEngine;

public class ThinkingAchievement : OnMessage<PieceMoved>
{
    [SerializeField] private Achievements achievements;

    private bool _hasStarted;
    private TilePoint _back;
    private int _count;

    protected override void Execute(PieceMoved msg)
    {
        if (!_hasStarted && msg.Piece.GetComponent<TeleportingPiece>() != null)
        {
            _hasStarted = true;
            _back = msg.From;
            _count = 1;
        }
        else if (msg.Piece.GetComponent<TeleportingPiece>() != null && msg.To.Equals(_back))
        {
            _back = msg.From;
            _count++;
            if (_count == 4)
                achievements.UnlockAchievement(AchievementType.Thinking);
        }
        else
        {
            _hasStarted = false;
            _count = 0;
        }
    }
}
