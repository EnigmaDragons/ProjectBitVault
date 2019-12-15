using UnityEngine;

[CreateAssetMenu]
public class GameLevel : ScriptableObject
{
    [SerializeField] private string levelName;
    [SerializeField] private GameObject prefab;
    [SerializeField] private StarObjective[] starObjectives;
    [SerializeField] private Dialogue openingDialogue;
    [SerializeField] private Dialogue closingDialogue;

    public string Name => levelName;
    public GameObject Prefab => prefab;
    public StarObjective[] StarObjectives => starObjectives;
    public Dialogue OpeningDialogue => openingDialogue;
    public Dialogue ClosingDialogue => closingDialogue;
}
