using UnityEngine;

[CreateAssetMenu]
public class GameLevel : ScriptableObject
{
    [SerializeField] private string levelName;
    [SerializeField] private GameObject prefab;
    [SerializeField] private StarObjective[] starObjectives;
    [SerializeField] private DialogueLine[] openingDialogue;
    [SerializeField] private DialogueLine[] closingDialogue;

    public string Name => levelName;
    public GameObject Prefab => prefab;
    public StarObjective[] StarObjectives => starObjectives;
    public DialogueLine[] OpeningDialogue => openingDialogue;
    public DialogueLine[] ClosingDialogue => closingDialogue;
}
