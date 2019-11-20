using UnityEngine;

public class GainStarOnNoMoreJumpables : OnMessage<LevelStateChanged>
{
    [SerializeField] private CurrentLevelMap map;
    [SerializeField] private GameObject collectedStar;

    private bool _awardedStar = false;

    protected override void Execute(LevelStateChanged msg)
    {
        if (!_awardedStar && map.NumOfJumpables == 0)
        {
            _awardedStar = true;
            Message.Publish(new StarCollected());
            var star = Instantiate(collectedStar, transform.parent.parent);
            star.transform.position = transform.position;
            gameObject.SetActive(false);
        }
    }
}
