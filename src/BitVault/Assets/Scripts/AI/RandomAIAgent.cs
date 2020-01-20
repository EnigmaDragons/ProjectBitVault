using System.Collections.Generic;

public class RandomAIAgent : IAIAgent
{
    public void Init(AIGameState startingState) {}
    public PieceMoved GetNextAction(List<PieceMoved> possibleActions) => possibleActions.Random();
    public void Update(AIGameState newState, int reward) {}
}
