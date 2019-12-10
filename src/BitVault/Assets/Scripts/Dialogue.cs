using UnityEngine;

[CreateAssetMenu]
public class Dialogue : ScriptableObject
{
    [SerializeField] private string dialogueName;
    [SerializeField] private DialogueLine[] lines;

    public string DialogueName => dialogueName;
    public DialogueLine[] Lines => lines;
}
