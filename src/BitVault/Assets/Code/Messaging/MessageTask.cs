using System.Collections.Generic;

public class MessageTask
{
    private readonly Maybe<object> _onCompleted;
    private readonly List<object> _slowSubscribers;
    private bool _completed = false;

    public MessageTask(Maybe<object> onCompleted, List<object> slowSubscribers)
    {
        _onCompleted = onCompleted;
        _slowSubscribers = slowSubscribers;
    }

    public void Done(object waitingOn)
    {
        _slowSubscribers.Remove(waitingOn);
        if (_slowSubscribers.Count == 0 && !_completed)
        {
            _onCompleted.IfPresent(Message.Publish);
            _completed = true;
        }
    }
}
