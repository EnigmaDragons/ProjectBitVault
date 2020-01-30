using UnityEngine;

[CreateAssetMenu]
public class Campaign : ScriptableObject
{
    [SerializeField] private string campaignName;
    [SerializeField] private GameLevels[] value;
    [SerializeField] private int newLevelsAvailable;

    public string Name => campaignName;
    public GameLevels[] Value => value;
    public int NewLevelsAvailable => newLevelsAvailable;
}
