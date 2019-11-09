using UnityEngine;

public class MovingPiece : OnSlowMessage<PieceMoved>
{
    [SerializeField] private float speed = 1f;

    private Vector3 _targetPosition;
    private MessageTask _task;

    protected override void Execute(PieceMoved msg, MessageTask task)
    {
        if (msg.Piece == gameObject)
        {
            _targetPosition = new Vector3(msg.To.X, msg.To.Y, 0);
            _task = task;
        }
        else
            task.Done(this);
    }

    private void Start() => _targetPosition = transform.position;

    private void Update()
    {
        if (transform.position != _targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, Time.deltaTime * speed);
            if (transform.position == _targetPosition)
                _task.Done(this);
        }
    }
}
