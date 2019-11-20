using UnityEngine;

[CreateAssetMenu]
public class Character : ScriptableObject
{
    [SerializeField] private string name;
    [SerializeField] private Sprite bust;

    public string Name => name;
    public Sprite Bust => bust;
}
