using System.Collections.Generic;

public class AISimulation
{
    public List<PieceMoved> GetPossibleActions() => new List<PieceMoved>();
    public AIGameState GetState() => new AIGameState();
    public int GetLastReward() => 0;
    public void MakeMove(PieceMoved move) => Message.Publish(move);
}
