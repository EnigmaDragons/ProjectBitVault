using UnityEngine;

public sealed class WinLevelWhenOnePegRemains : MonoBehaviour
{
    [SerializeField] private CurrentLevelMap map;
    [SerializeField] private GameEvent win;

    void Update()
    {
        if (map.NumSelectableObjects <= 1)
            win.Publish();
    }
}
