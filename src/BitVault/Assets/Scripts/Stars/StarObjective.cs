using UnityEngine;

[CreateAssetMenu]
public class StarObjective : ScriptableObject
{
    [SerializeField] private string objective;
    public string Objective => objective;
}
