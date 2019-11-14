using UnityEngine;

[CreateAssetMenu]
public sealed class UserInputMessages : ScriptableObject
{
    public void RequestLevelReset() => Message.Publish(new LevelResetRequested());
}
