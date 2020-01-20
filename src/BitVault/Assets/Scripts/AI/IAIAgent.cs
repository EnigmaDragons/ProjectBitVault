using System.Collections.Generic;

public interface IAIAgent
{
    void Init(AIGameState startingState);
    PieceMoved GetNextAction(List<PieceMoved> possibleActions);
    void Update(AIGameState newState, int reward);
}
