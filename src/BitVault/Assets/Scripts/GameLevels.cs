using UnityEngine;

public sealed class GameLevels : ScriptableObject
{
    [SerializeField] private GameObject[] value;

    public GameObject[] Value => value;
}
