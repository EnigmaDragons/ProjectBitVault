using UnityEngine;

[CreateAssetMenu]
public class GameZones : ScriptableObject
{
    [SerializeField] private GameLevels[] value;

    public GameLevels[] Value => value;
}
