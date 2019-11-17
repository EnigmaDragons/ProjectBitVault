using UnityEngine;

public class RewardStars : MonoBehaviour
{
    [SerializeField] private CurrentLevelStars stars;
    [SerializeField] private GameObject[] starObjects;

    private void Awake()
    {
        for (var i = 0; i < stars.Count; i++)
            starObjects[i].SetActive(true);
    }
}
