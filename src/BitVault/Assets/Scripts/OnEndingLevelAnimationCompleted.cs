using UnityEngine;

public class OnEndingLevelAnimationCompleted : OnMessage<EndingLevelAnimationFinished>
{
    [SerializeField] private Navigator navigator;
    [SerializeField] private BoolVariable isLevelStart;
    [SerializeField] private BoolReference AutoSkipStory;

    protected override void Execute(EndingLevelAnimationFinished msg)
    {
        isLevelStart.Value = false;
        if (AutoSkipStory.Value)
            navigator.NavigateToRewards();
        else
            navigator.NavigateToDialogue();
    }
}
