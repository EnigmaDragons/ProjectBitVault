using System.Linq;
using UnityEngine;

public class StarsUIPresenter : MonoBehaviour
{
    private int _numStarsCollected;
    
    [SerializeField] private StarCounterPresenter[] stars;

    private void OnEnable()
    {
        Message.Subscribe<StarCollected>(_ => AddCollectedStar(), this);
        Message.Subscribe<LevelReset>(_ => Reset(), this);
    }

    private void OnDisable() => Message.Unsubscribe(this);

    private void Reset()
    {
        stars.ForEach(s => s.SetState(false));
        _numStarsCollected = 0;
    }
    
    private void AddCollectedStar()
    {
        _numStarsCollected++;
        Enumerable.Range(0, _numStarsCollected).ForEach(i => stars[i].SetState(true));
    }
}
  
