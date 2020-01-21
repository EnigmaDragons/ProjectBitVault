using System.Collections.Generic;
using System.Linq;

public class AIBrute
{
    private List<LevelSimulationSnapshot> _oldStates;
    private List<AIMove> _movesToWin;

    public bool CalculateSolution(LevelSimulationSnapshot state)
    {
        _oldStates = new List<LevelSimulationSnapshot>();
        _movesToWin = new List<AIMove>();
        return RecursiveCalculateSolution(state);
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
                    _movesToWin.Prepend(move);
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
