
using UnityEngine;

public class RegisterAsWalkableTile : MonoBehaviour
{
    [SerializeField] private CurrentLevelMap currentLevelMap;

    void Start()
    {
        currentLevelMap.RegisterWalkableTile(gameObject);
    }
}