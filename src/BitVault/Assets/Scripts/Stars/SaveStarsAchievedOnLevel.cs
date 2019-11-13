using UnityEngine;

public class SaveStarsAchievedOnLevel : MonoBehaviour
{
    [SerializeField] private CurrentLevelStars stars;
    [SerializeField] private CurrentLevel level;
    [SerializeField] private SaveStorage storage;

    private void Awake() => storage.SaveStars(level.ActiveLevel, stars.Count);
}
