using UnityEngine;

public class ZoneTutorialButton : MonoBehaviour
{
    [SerializeField] private CurrentTutorial tutorial;
    [SerializeField] private Dialogue[] zoneTutorials;

    public void Init(int zoneIndex)
    {
        gameObject.SetActive(zoneIndex < zoneTutorials.Length);
        if (zoneIndex < zoneTutorials.Length)
            tutorial.Set(zoneTutorials[zoneIndex]);
    }
}
