using UnityEngine;

public sealed class DisableIfHintsNotAvailable : MonoBehaviour
{
    [SerializeField] private CurrentLevel level;
    [SerializeField] private GameObject[] targets;

    void Awake()
    {
        if (!level.ActiveLevel.Solution.IsPresent)
            targets.ForEach(t => t.SetActive(false));
    }
}
