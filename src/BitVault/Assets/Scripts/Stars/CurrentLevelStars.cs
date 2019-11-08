using UnityEngine;

[CreateAssetMenu]
public class CurrentLevelStars : ScriptableObject
{
    [SerializeField] private int count;

    public int Count => count;
    public void Increment() => count++;
    public void Reset() => count = 0;
}