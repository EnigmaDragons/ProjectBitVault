using UnityEngine;

public class CurrentSelectedPiece : ScriptableObject
{
    [SerializeField] private Maybe<GameObject> selected;

    public Maybe<GameObject> Selected => selected;

    public void Select(GameObject obj) => selected = obj;
    public void Deselect() => selected = null;
}
