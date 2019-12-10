using UnityEngine;

public class CurrentLevelStars : ScriptableObject
{
    [SerializeField] private int count;

    public int Count => count;
    public void Increment() => count++;
    public void Decrement() => count--;
    public void Reset() => count = 0;
}
