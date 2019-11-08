using UnityEngine;

public class GainStarOnActivationIfAdjacent : MonoBehaviour, Activatable
{
    [SerializeField] private CurrentLevelMap map;

    private void Start() => map.RegisterActivatable(this);

    public bool CanActivate(GameObject piece) => new TilePoint(piece).IsAdjacentTo(new TilePoint(gameObject));
    
    public GameObject GameObject => gameObject;
    
    public void Activate()
    {
        Message.Publish(new StarCollected());
        Message.Publish(new ObjectDestroyed(gameObject));
    }
}
