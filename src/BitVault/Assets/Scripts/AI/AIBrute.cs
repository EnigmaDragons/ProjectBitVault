using System.Collections.Generic;
using System.Linq;

public class AIBrute
{
    private List<LevelSimulationSnapshot> _oldStates;
    private List<AIMove> _movesToWin;
    
    public bool CanWin { get; private set; }
    public AIMove NextMove => _movesToWin.Last();
    
    public bool CalculateSolution(LevelSimulationSnapshot state)
    {
        _oldStates = new List<LevelSimulationSnapshot>();
        _movesToWin = new List<AIMove>();
        CanWin = RecursiveCalculateSolution(state);
        return true;
    }

    private bool RecursiveCalculateSolution(LevelSimulationSnapshot state)
    {
        _oldStates.Add(state);
        foreach (var move in state.GetMoves().ToArray().Shuffled())
        {
            var newState = state.MakeMove(move);
            if (newState.IsGameOver())
            {
                if (newState.HasWon())
                {
                    _movesToWin.Add(move);
                    return true;
                }
            }
            else if (!_oldStates.Any(x => x.Equals(newState)) && RecursiveCalculateSolution(newState))
            {
                _movesToWin.Add(move);
                    return true;
            }
        }
        return false;
    }
}
