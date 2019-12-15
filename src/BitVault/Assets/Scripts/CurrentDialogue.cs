using UnityEngine;

[CreateAssetMenu]
public class CurrentDialogue : ScriptableObject
{
    [SerializeField] private ConjoinedDialogues dialogue;

    public ConjoinedDialogues Dialogue => dialogue;

    public void Set(ConjoinedDialogues story) => dialogue = story;
}
