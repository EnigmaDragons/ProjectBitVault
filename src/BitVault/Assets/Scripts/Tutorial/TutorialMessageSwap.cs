using UnityEngine;

public class TutorialMessageSwap : MonoBehaviour
{
    private Message.MessageQueue _normalQueue;
    private void Awake() => _normalQueue = Message.SwapMessageQueues(new Message.MessageQueue());
    private void OnDestroy() => Message.SwapMessageQueues(_normalQueue);
}
