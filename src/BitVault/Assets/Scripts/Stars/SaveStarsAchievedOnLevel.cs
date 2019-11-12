using UnityEngine;

public class SaveStarsAchievedOnLevel : MonoBehaviour
{
    [SerializeField] private CurrentLevelStars stars;
    [SerializeField] private CurrentLevel level;

    private void Awake()
    {
        var key = StringValues.StarsForLevel(level.ActiveLevelName);
        if (!PlayerPrefs.HasKey(key) || PlayerPrefs.GetInt(key) < stars.Count)
            PlayerPrefs.SetInt(key, stars.Count);
    } 
}
