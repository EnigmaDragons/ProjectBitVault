
public sealed class GainStarOnLevelComplete : OnMessage<LevelCompleted>
{
    protected override void Execute(LevelCompleted msg) => Message.Publish(new StarCollected());
}
