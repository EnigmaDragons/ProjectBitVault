using UnityEngine;

public class DestroyIfDoubleJumped : OnMessage<PieceMoved>
{
    [SerializeField] private GameObject deactivateOnFirstJump;
    [SerializeField] private GameObject activateOnFirstJump;
    [ReadOnly, SerializeField] private int numJumpsRemaining = 2;

    protected override void Execute(PieceMoved msg)
    {
        if (msg.From.IsAdjacentTo(new TilePoint(gameObject)) && msg.To.IsAdjacentTo(new TilePoint(gameObject)) && (msg.To.X == msg.From.X || msg.To.Y == msg.From.Y))
            UpdateStateAfterJumped();
    }

    private void UpdateStateAfterJumped()
    {
        numJumpsRemaining--;
        if (numJumpsRemaining == 0)
            Message.Publish(new ObjectDestroyed(gameObject));
        else
        {
            deactivateOnFirstJump.SetActive(false);
            activateOnFirstJump.SetActive(true);
        }
    }
}
