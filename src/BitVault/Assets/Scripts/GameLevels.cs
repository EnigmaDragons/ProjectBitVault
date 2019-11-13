using UnityEngine;

[CreateAssetMenu]
public sealed class GameLevels : ScriptableObject
{
    [SerializeField] private GameLevel[] value;
    [SerializeField] private IntReference starsRequired;

    public GameLevel[] Value => value;
    public int StarsRequired => starsRequired;
}
