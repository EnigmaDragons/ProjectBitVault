using System;
using System.Linq;
using UnityEngine;

public class HintExecutor : OnMessage<LevelReset, PieceMoved>
{
    [SerializeField] private SaveStorage storage;
    [SerializeField] private LockBoolVariable gameInputActive;
    [SerializeField] private CurrentLevel level;
    [SerializeField] private float secsBetweenMoves;
    [SerializeField] private CurrentLevelMap map;

    private int _hintCount;
    private bool _showingHints;
    private float _t;
    private int _currentHint;

    public bool CanGiveHint { get; private set; }

    public void AddHint()
    {
        gameInputActive.Lock(gameObject);
        storage.AddHintToLevel(level.ActiveLevel);
        _hintCount++;
        _showingHints = true;
        _t = secsBetweenMoves;
    }

    protected override void Execute(LevelReset msg)
    {
        Start();
    }

    protected override void Execute(PieceMoved msg)
    {
        if (!_showingHints)
            CanGiveHint = false;
    }

    private void Start()
    {
        CanGiveHint = true;
        _hintCount = storage.GetHints(level.ActiveLevel);
        if (storage.GetUseHints() && _hintCount > 0 && level.ActiveLevel.Solution.IsPresent)
        {
            gameInputActive.Lock(gameObject);
            _showingHints = true;
            _currentHint = 0;
            _t = secsBetweenMoves;
        }
    }

    private void Update()
    {
        if (!_showingHints)
            return;
        _t = Math.Max(0, _t - Time.deltaTime);
        if (_t == 0)
        {
            if (_currentHint == _hintCount)
            {
                _showingHints = false;
                gameInputActive.Unlock(gameObject);
            }
            else
            {
                var hint = level.ActiveLevel.Solution.Value.Hints[_currentHint];
                _currentHint++;
                _t = secsBetweenMoves;
                Message.Publish(new PieceMoved(map.Selectables.First(x => hint.From.Equals(new TilePoint(x))), hint.From, hint.To));
            }
        }
    }
}
