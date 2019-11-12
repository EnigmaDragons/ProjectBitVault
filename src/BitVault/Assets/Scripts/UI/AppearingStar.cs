using UnityEngine;
using UnityEngine.UI;

public class AppearingStar : MonoBehaviour
{
    [SerializeField] private int number;
    [SerializeField] private CurrentLevelStars stars;
    [SerializeField] private Image star;
    [SerializeField] private Sprite filledStar;

    private void Awake()
    {
        if (stars.Count >= number)
            star.sprite = filledStar;
    }
}
