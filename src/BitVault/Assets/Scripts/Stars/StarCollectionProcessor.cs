using UnityEngine;

public class StarCollectionProcessor : OnMessage<StarCollected, UndoStarCollected>
{
    [SerializeField] private CurrentLevelStars stars;
    
    protected override void Execute(StarCollected msg) => stars.Increment();
    protected override void Execute(UndoStarCollected msg) => stars.Decrement();
}
