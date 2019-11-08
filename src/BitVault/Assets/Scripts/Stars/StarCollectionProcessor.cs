using UnityEngine;

public class StarCollectionProcessor : OnMessage<StarCollected>
{
    [SerializeField] private CurrentLevelStars stars;
    
    protected override void Execute(StarCollected msg) => stars.Increment();
}
