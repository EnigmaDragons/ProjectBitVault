using UnityEngine;

public class AIOrchestrator : MonoBehaviour
{
    [SerializeField] private BoolVariable gameInputActive;

    private IAIAgent _agent = new RandomAIAgent();
    private AISimulation _simulation = new AISimulation();
    private bool _makingMove;

    private void Start()
    {
        _agent.Init(new AIGameState());
    }

    private void Update()
    {
        if (gameInputActive.Value)
        {
            _agent.Update(, );
            Message.Publish(_agent.GetNextAction(_simulation.GetPossibleActions()));
        }
    }
}
