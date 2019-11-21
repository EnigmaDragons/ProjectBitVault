using UnityEngine;

public sealed class CollectStarOnEntered : OnMessage<PieceMoved>
{
    [SerializeField] private GameObject collectedStar;

    protected override void Execute(PieceMoved msg)
    {
        if (!new TilePoint(gameObject).Equals(msg.To)) return;
        Message.Publish(new StarCollected());
        Message.Publish(new ObjectDestroyed(gameObject));
        var star = Instantiate(collectedStar, transform.parent);
        star.transform.localPosition = transform.localPosition;
    }
}
