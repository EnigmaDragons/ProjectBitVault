using UnityEngine;

public class TutorialMessageSwap : MonoBehaviour
{
    private Message.MessageQueue _normalQueue;
    private void OnEnable() => _normalQueue = Message.SwapMessageQueues(new Message.MessageQueue());
    private void OnDisable() => Message.SwapMessageQueues(_normalQueue);
}
